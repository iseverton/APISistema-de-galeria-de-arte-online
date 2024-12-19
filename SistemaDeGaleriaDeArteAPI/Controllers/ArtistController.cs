using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.Services;
using SistemaDeGaleriaDeArteAPI.ViewModels;
using System.Security.Claims;

namespace SistemaDeGaleriaDeArteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtistController : Controller
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public ArtistController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpGet("obras")]
    public async Task<ActionResult<List<WorkModel>>> Get() {
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

    [HttpGet("obras/{id}")]
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

    [HttpPost("criarwork")]
    [Authorize(Roles = "admin,moderator,artist")]
    public async Task<IActionResult> createWork(WorkViewModel model) 
    { 
        if(model is null)
        {
           return BadRequest("Preencha as informacoes");
        }

        try
        {
            var work = new WorkModel(
                 model.NameWork,
                 model.Description,
                 model.ImageUrl,
                 DateTime.UtcNow,
                 DateTime.UtcNow,
                 model.IsAvailable,
                 model.CategoryId,
                 model.ArtistId
                );

            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = work.WorkId }, work);
        }
        catch (MySqlException Bd)
        {
            return BadRequest($"Falao ao salvar no banco! {Bd.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha interna! {ex.Message}");
        }
    }

    [HttpPut("editar/{id:int}")]
    [Authorize(Roles = "admin,moderator,artist")]
    public async Task<IActionResult> EditWork(EditarWorkViewModel EditWork,int id) 
    {

        // pegar id do usuario autenticado
        var userIdClaim = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value);


        // verificar se usuario tem permisao para editar
        var work = await _context.Works.FirstOrDefaultAsync(w => w.WorkId == id);
        if(work == null)
        {
            return BadRequest("obra nao encontrada");
        }
        
        if (work.ArtistUserID != userIdClaim) 
        {
            return Unauthorized("Você não tem permissão para editar essa obra.");
        }



        try
        {
            work.NameWork = EditWork.NameWork;
            work.Description = EditWork.Description;
            work.ImageUrl = EditWork.ImageUrl;
            work.IsAvailable = EditWork.IsAvailable;
            work.LastUpdateDate = DateTime.UtcNow;


            await _context.SaveChangesAsync();
            return Ok(EditWork);
        }
        catch (MySqlException Bd)
        {
            return BadRequest($"Falao ao salvar no banco! {Bd.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha interna! {ex.Message}");
        }
    }


    [HttpDelete("deletar/{id}")]
    [Authorize(Roles = "admin,moderator,artist")]
    public async Task<IActionResult> DeleteWok(int id)
    {
        var userIdClaim = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value);

        var work = await _context.Works.FirstOrDefaultAsync(w => w.WorkId == id);
        if (work == null)
        {
            return BadRequest("obra nao encontrada");
        }

        if (work.ArtistUserID != userIdClaim)
        {
            return Unauthorized("Você não tem permissão para editar essa obra.");
        }
        try
        {
            _context.Works.Remove(work);
            await _context.SaveChangesAsync();


            return Ok("Work apagado!");
        }
        catch (MySqlException Bd)
        {
            return BadRequest($"Falao ao salvar no banco! {Bd.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha interna! {ex.Message}");
        }
    }

}
