using AdocaoPetApi.Data;
using AdocaoPetApi.DTOs;
using AdocaoPetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace AdocaoPetApi.Controllers
{
    [ApiController, Route("api")]
    [Authorize(Roles = "Admin")]
    public class CategoriaAnimalController(DataContext context, IMemoryCache cache) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly IMemoryCache _cache = cache;
        private const string CATEGORIA_CACHE_KEY = "CategoriaAnimais";

        [HttpGet("v1/categorias")]
        public async Task<IActionResult> GetAsync(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25
        )
        {
            try
            {
                if(!_cache.TryGetValue(CATEGORIA_CACHE_KEY, out List<CategoriaAnimal> categorias))
                {
                    categorias = await _context.CategoriaAnimals
                                        .AsNoTracking()
                                        .Skip(page * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                    _cache.Set(CATEGORIA_CACHE_KEY, categorias, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12),
                        SlidingExpiration = TimeSpan.FromHours(6)
                    });
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

        [HttpGet("v1/categorias/{id:int}")]
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

        [HttpDelete("v1/categorias/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var categoria = await _context.CategoriaAnimals
                                        .FirstOrDefaultAsync(x => x.Id == id);

                if(categoria == null)
                    return NotFound(new ResultDTO<string>("Categoria não encontrada"));
                
                _context.Remove(categoria);
                await _context.SaveChangesAsync();
                _cache.Remove(CATEGORIA_CACHE_KEY);
                
                return Ok(new ResultDTO<CategoriaAnimal>(categoria));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<string>("HGYP41 - Erro interno no servidor"));
            }
        }
    }
}