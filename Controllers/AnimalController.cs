using System.Diagnostics.CodeAnalysis;
using AdocaoPetApi.Data;
using AdocaoPetApi.DTOs;
using AdocaoPetApi.DTOs.Animal;
using AdocaoPetApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdocaoPetApi.Controllers
{
    [Route("api")]
    public class AnimalController : ControllerBase
    {
        private readonly DataContext _context;

        public AnimalController(DataContext context)
        => _context = context;

        [HttpGet("v1/animal")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var animais = await _context.Animais
                                    .AsNoTracking()
                                    .Include(u => u.Usuario)
                                    .Select(x => new GetAnimalDTO
                                    {
                                        Id = x.Id,
                                        Nome = x.Usuario.Nome,
                                        Telefone = x.Usuario.Telefone,
                                        Especie = x.Especie,
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
                                    .ToListAsync();

                return Ok(new ResultDTO<List<GetAnimalDTO>>(animais));
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
                                    .Where(x => x.Id == id)
                                    .Select(x => new GetAnimalDTO
                                    {
                                        Id = x.Id,
                                        Nome = x.Usuario.Nome,
                                        Telefone = x.Usuario.Telefone,
                                        Especie = x.Especie,
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

        [HttpPost("v1/animal")]
        public async Task<IActionResult> PostAsync(
            [FromBody] PostAnimalDTO model)
        {
            try
            {
                var animal = new Animal
                {
                    UsuarioId = model.UsuarioId,
                    Especie = model.Especie,
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
            try
            {
                var animal = await _context.Animais
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if(animal == null)
                    return NotFound("Animal não encontrado.");

                animal.Especie = model.Especie;
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