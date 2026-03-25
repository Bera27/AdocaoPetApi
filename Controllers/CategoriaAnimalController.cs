using AdocaoPetApi.Data;
using AdocaoPetApi.DTOs;
using AdocaoPetApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace AdocaoPetApi.Controllers
{
    [ApiController, Route("api")]
    public class CategoriaAnimalController : ControllerBase
    {
        private readonly DataContext _context;
        private const string CATEGORIA_CACHE_KEY = "CategoriaAnimais";

        public CategoriaAnimalController(DataContext context)
            => _context = context;

        
        [HttpGet("v1/Categorias")]
        public async Task<IActionResult> GetAsync(
            [FromServices] IMemoryCache cache,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25
        )
        {
            try
            {
                if(!cache.TryGetValue(CATEGORIA_CACHE_KEY, out List<CategoriaAnimal> categorias))
                {
                    categorias = await _context.CategoriaAnimals
                                        .AsNoTracking()
                                        .Skip(page * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                    var cacheOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                    cache.Set(CATEGORIA_CACHE_KEY, categorias, cacheOptions);
                }

                return Ok(new ResultDTO<dynamic>(new
                {
                    page,
                    pageSize,
                    categorias
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("JPO87 - Erro interno no servidor"));
            }
        }

        [HttpGet("v1/Categorias/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var categoria = await _context.CategoriaAnimals
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

                if(categoria == null)
                    return NotFound("Categoria não encontrada");

                return Ok(new ResultDTO<CategoriaAnimal>(categoria));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("JKUG14 - Erro interno no servidor"));
            }
        }
    }
}