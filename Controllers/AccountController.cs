using AdocaoPetApi.Data;
using AdocaoPetApi.DTOs;
using AdocaoPetApi.DTOs.Usuario;
using AdocaoPetApi.Models;
using AdocaoPetApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace AdocaoPetApi.Controllers
{
    
    [ApiController, Route("api/")]
    public class AccountController(TokenService tokenService, DataContext context) : ControllerBase
    {
        private readonly TokenService _tokenService = tokenService;
        private readonly DataContext _context = context;

        [HttpPost("v1/accounts")]
        public async Task<IActionResult> Post(
            [FromBody] RegistrarDTO model)
        {
            if(model.NomeRole.Equals("Admin"))
                return BadRequest(new ResultDTO<string>("Não é permitido criar administradores"));

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Nome == model.NomeRole);

            if(role == null)
                return NotFound("O perfil/Role informado não existe.");


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