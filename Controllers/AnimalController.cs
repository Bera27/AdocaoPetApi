using AdocaoPetApi.Data;
using AdocaoPetApi.DTOs;
using AdocaoPetApi.DTOs.Animal;
using AdocaoPetApi.Extensions;
using AdocaoPetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdocaoPetApi.Controllers
{
    [ApiController, Route("api")]
    [Authorize(Roles = "Admin, Doador")]
    public class AnimalController : ControllerBase
    {
        private readonly DataContext _context;

        public AnimalController(DataContext context)
        => _context = context;

        [HttpGet("v1/animal")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var animais = await _context.Animais
                                    .AsNoTracking()
                                    .Include(u => u.Usuario)
                                    .Include(x => x.CategoriaAnimal)
                                    .Select(x => new GetAnimalDTO
                                    {
                                        Id = x.Id,
                                        Nome = x.Usuario.Nome,
                                        Telefone = x.Usuario.Telefone,
                                        Categoria = x.CategoriaAnimal.NomeCategoria,
                                        Raca = x.Raca,
                                        Idade = x.Idade,
                                        Sexo = x.Sexo,
                                        Descricao = x.Descricao,
                                        Porte = x.Porte,
                                        Saude = x.Saude,
                                        Historia = x.Historia,
                                        DataCadastro = x.DataCadastro,
                                        Status = x.Status,
                                        FotoUrl = x.FotoUrl
                                    })
                                    .Skip(page * pageSize)
                                    .Take(pageSize)
                                    .OrderByDescending(x => x.DataCadastro)
                                    .ToListAsync();

                return Ok(new ResultDTO<dynamic>(new
                {
                    page,
                    pageSize,
                    animais
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("ASAW10 - Erro interno no servidor"));
            }
        }

        [HttpGet("v1/animal/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id)
        {
            try
            {
                var animal = await _context.Animais
                                    .AsNoTracking()
                                    .Include(u => u.Usuario)
                                    .Include(x => x.CategoriaAnimal)
                                    .Where(x => x.Id == id)
                                    .Select(x => new GetAnimalDTO
                                    {
                                        Id = x.Id,
                                        Nome = x.Usuario.Nome,
                                        Telefone = x.Usuario.Telefone,
                                        Categoria = x.CategoriaAnimal.NomeCategoria,
                                        Raca = x.Raca,
                                        Idade = x.Idade,
                                        Sexo = x.Sexo,
                                        Descricao = x.Descricao,
                                        Porte = x.Porte,
                                        Saude = x.Saude,
                                        Historia = x.Historia,
                                        DataCadastro = x.DataCadastro,
                                        Status = x.Status,
                                        FotoUrl = x.FotoUrl
                                    })
                                    .FirstOrDefaultAsync();

                if(animal == null)
                    return NotFound(new ResultDTO<string>("Animal não encontrado."));

                return Ok(new ResultDTO<GetAnimalDTO>(animal));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("SSA20 - Erro interno no servidor"));
            }
        }

        [HttpGet("v1/animal/categoria/{nomeCategoria}")]
        public async Task<IActionResult> GetCategoria(
            [FromRoute] string nomeCategoria,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var animais = await _context.Animais
                                        .AsNoTracking()
                                        .Include(x => x.Usuario)
                                        .Include(x => x.CategoriaAnimal)
                                        .Where(x => x.CategoriaAnimal.NomeCategoria == nomeCategoria)
                                        .Select(x => new GetAnimalDTO
                                        {
                                            Id = x.Id,
                                            Nome = x.Usuario.Nome,
                                            Telefone = x.Usuario.Telefone,
                                            Categoria = x.CategoriaAnimal.NomeCategoria,
                                            Raca = x.Raca,
                                            Idade = x.Idade,
                                            Sexo = x.Sexo,
                                            Descricao = x.Descricao,
                                            Porte = x.Porte,
                                            Saude = x.Saude,
                                            Historia = x.Historia,
                                            DataCadastro = x.DataCadastro,
                                            Status = x.Status,
                                            FotoUrl = x.FotoUrl
                                        })
                                        .OrderByDescending(x => x.DataCadastro)
                                        .Skip(page * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                return Ok(new ResultDTO<dynamic>(new
                {
                    page,
                    pageSize,
                    animais
                }));
            }
            catch(Exception)
            {
                return StatusCode(500, new ResultDTO<string>("LMHG12 - Erro interno no servidor"));
            }
        }

        [HttpGet("v1/animal/raca/{raca}")]
        public async Task<IActionResult> GetRaca(
            [FromRoute] string raca,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var animais = await _context.Animais
                                    .AsNoTracking()
                                    .Include(x => x.Usuario)
                                    .Include(x => x.CategoriaAnimal)
                                    .Where(x => x.Raca == raca)
                                    .Select(x => new GetAnimalDTO
                                    {
                                        Id = x.Id,
                                        Nome = x.Usuario.Nome,
                                        Telefone = x.Usuario.Telefone,
                                        Categoria = x.CategoriaAnimal.NomeCategoria,
                                        Raca = x.Raca,
                                        Idade = x.Idade,
                                        Sexo = x.Sexo,
                                        Descricao = x.Descricao,
                                        Porte = x.Porte,
                                        Saude = x.Saude,
                                        Historia = x.Historia,
                                        DataCadastro = x.DataCadastro,
                                        Status = x.Status,
                                        FotoUrl = x.FotoUrl
                                    })
                                    .OrderByDescending(x => x.DataCadastro)
                                    .Skip(page * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
                
                return Ok(new ResultDTO<dynamic>(new
                {
                    page,
                    pageSize,
                    animais
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("JHFGR15 - Erro interno no servidor"));
            }
        }

        [HttpPost("v1/animal")]
        public async Task<IActionResult> PostAsync(
            [FromBody] PostAnimalDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));

            try
            {
                var animal = new Animal
                {
                    UsuarioId = model.UsuarioId,
                    IdCategoriaAnimal = model.IdCategoriaAnimal,
                    Raca = model.Raca,
                    Idade = model.Idade,
                    Sexo = model.Sexo,
                    Descricao = model.Descricao,
                    Porte = model.Porte,
                    Saude = model.Saude,
                    Historia = model.Historia,
                    Status = model.Status,
                    FotoUrl = model.FotoUrl
                };

                await _context.Animais.AddAsync(animal);
                await _context.SaveChangesAsync();

                return Created($"api/v1/animal/{animal.Id}", new ResultDTO<Animal>(animal));
            }
            catch(DbUpdateException)
            {
                return StatusCode(500, new ResultDTO<string>("HHAW21 - Não foi possível incluir o animal"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("AFAF30 - Erro interno no servidor"));
            }
        }

        [HttpPut("v1/animal/{id:guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] PostAnimalDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));

            try
            {
                var animal = await _context.Animais
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if(animal == null)
                    return NotFound("Animal não encontrado.");

                animal.IdCategoriaAnimal = model.IdCategoriaAnimal;
                animal.Raca = model.Raca;
                animal.Idade = model.Idade;
                animal.Sexo = model.Sexo;
                animal.Descricao = model.Descricao;
                animal.Porte = model.Porte;
                animal.Saude = model.Saude;
                animal.Historia = model.Historia;
                animal.Status = model.Status;
                animal.FotoUrl = model.FotoUrl;

                _context.Animais.Update(animal);
                await _context.SaveChangesAsync();

                return Created($"api/v1/animal/{animal.Id}", new ResultDTO<Animal>(animal));                
            }
            catch(DbUpdateException)
            {
                return StatusCode(500, new ResultDTO<string>("HHAW21 - Não foi possível Alterar o animal"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("ARAW40 - Erro interno no servidor"));
            }
        }

        [HttpDelete("v1/animal/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id)
        {
            try
            {
                var animal = await _context.Animais
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if(animal == null)
                    return NotFound(new ResultDTO<string>("Animal não encontrado."));

                _context.Animais.Remove(animal);
                await _context.SaveChangesAsync();

                return Ok(new ResultDTO<Animal>(animal));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("ADAS50 - Erro interno no servidor"));
            }
        }
    }
}