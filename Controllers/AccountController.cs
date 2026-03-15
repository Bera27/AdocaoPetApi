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
            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                Telefone = model.Telefone,
                Senha = model.Senha
            };

            usuario.Senha = PasswordHasher.Hash(model.Senha);

            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return Ok(new ResultDTO<dynamic>(new
                {
                    usuario = usuario.Email, usuario.Senha
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