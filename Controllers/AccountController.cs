using AdocaoPetApi.Data;
using AdocaoPetApi.DTOs;
using AdocaoPetApi.DTOs.Usuario;
using AdocaoPetApi.Extensions;
using AdocaoPetApi.Models;
using AdocaoPetApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SecureIdentity.Password;

namespace AdocaoPetApi.Controllers
{
    
    [ApiController, Route("api/")]
    public class AccountController(TokenService tokenService, DataContext context, IMemoryCache cache) : ControllerBase
    {
        private readonly TokenService _tokenService = tokenService;
        private readonly DataContext _context = context;
        private readonly IMemoryCache _cache = cache;

        [HttpPost("v1/accounts")]
        public async Task<IActionResult> Post(
            [FromBody] RegistrarDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));

            if(model.NomeRole.Equals("Admin"))
                return BadRequest(new ResultDTO<string>("Não é permitido criar administradores"));

            var cacheKey = $"role:{model.NomeRole}";

            if(!_cache.TryGetValue(cacheKey, out Role? role))
            {
                role = await _context.Roles
                    .FirstOrDefaultAsync(x => x.Nome == model.NomeRole);
                
                if(role != null)
                {
                    _cache.Set(cacheKey, role, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12),
                        SlidingExpiration = TimeSpan.FromHours(6)
                    });
                }
            }

            if(role == null)
                return NotFound(new ResultDTO<string>("O perfil/Role informado não existe."));


            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                Telefone = model.Telefone,
                Senha = PasswordHasher.Hash(model.Senha)
            };

            usuario.Roles.Add(role);

            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return Ok(new ResultDTO<dynamic>(new
                {
                    usuario = usuario.Email,
                    perfil = role.Nome
                }));
            }
            catch(DbUpdateException)
            {
                return StatusCode(500, new ResultDTO<string>("Email já cadastrado"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("AWFA10 - Erro interno no servidor"));
            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDTO model)
        {
            var usuario = await _context.Usuarios
                                    .AsNoTracking()
                                    .Include(x => x.Roles)
                                    .FirstOrDefaultAsync(x => x.Email == model.Email);

            if(usuario == null)
                return StatusCode(401, new ResultDTO<string>("Email ou Senha inválidos"));
            
            if(!PasswordHasher.Verify(usuario.Senha, model.Senha))
                return StatusCode(401, new ResultDTO<string>("Email ou Senha inválidos"));

            try
            {
                var token = _tokenService.GenerateToken(usuario);

                return Ok(new ResultDTO<string>(token, null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("WJFD10 - Erro interno no servidor"));
            }
            
        }
    }
}