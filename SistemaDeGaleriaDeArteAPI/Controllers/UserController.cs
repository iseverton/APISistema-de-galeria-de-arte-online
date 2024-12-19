using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.Services;

namespace SistemaDeGaleriaDeArteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public UserController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("obras")]
        public async Task<ActionResult<List<WorkModel>>> Get()
        {
            try
            {
                var works = await _context.Works.ToListAsync();
                if (works is null)
                {
                    NotFound("nenhuma obra foi encontrada");
                }
                return Ok(works);
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha interna! {ex.Message}");
            }
        }

        [HttpGet("obra/{id}")]
        public async Task<ActionResult<List<WorkModel>>> GetWorkById(int id)
        {
            try
            {
                var works = await _context.Works.FirstOrDefaultAsync(w => w.WorkId == id);
                if (works is null)
                {
                    NotFound("nenhuma obra foi encontrada");
                }
                return Ok(works);
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha interna! {ex.Message}");
            }
        }
    }
}
