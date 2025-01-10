using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Interfaces;
using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.Services;
using SistemaDeGaleriaDeArteAPI.ViewModels;
using System.Security.Claims;

namespace SistemaDeGaleriaDeArteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkController : Controller
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly IWorkRepository _WorkRepository;

    public WorkController(AppDbContext context, TokenService tokenService, IWorkRepository workRepository)
    {
        _context = context;
        _tokenService = tokenService;
        _WorkRepository = workRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkAll() {
        try
        {
            var works = await _WorkRepository.GetAsync();
            if (works is null)
            {
                NotFound("nenhuma obra foi encontrada");
            }
            return Ok(new ResultViewModels<List<WorkModel>>(works));
        }
        catch (MySqlException bd) 
        { 
            return StatusCode(500, new ResultViewModels<string>($"Erro ao acessar o banco de dados: {bd.Message}"));
        }
        catch (Exception ex)
        {
            return StatusCode(500,new ResultViewModels<string>($"Erro interno no servidor: {ex.Message}"));
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<List<WorkModel>>> GetWorkById(int id)
    {
        try
        {
            var work = await _WorkRepository.GetByIdAsync(id);
            if (work is null)
            {
                NotFound("nenhuma obra foi encontrada");
            }
            return Ok(new ResultViewModels<WorkModel>(work));
        }
        catch (MySqlException bd)
        {
            return StatusCode(500, new ResultViewModels<string>($"Erro ao acessar o banco de dados: {bd.Message}"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModels<string>($"Erro interno no servidor: {ex.Message}"));
        }
    }

    [HttpPost]
    [Authorize(Roles = "admin,moderator,artist")]
    public async Task<IActionResult> CreateWork(WorkViewModel model) 
    { 
        if(model is null)
        {
           return BadRequest("Preencha as informacoes");
        }

        try
        {
           var work = await _WorkRepository.PostAsync(model);
            return CreatedAtAction(nameof(GetWorkById), new { id = work.WorkId },new ResultViewModels<WorkModel>(work));
        }
        catch (MySqlException bd)
        {
            return StatusCode(500, new ResultViewModels<string>($"Erro ao acessar o banco de dados: {bd.Message}"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModels<string>($"Erro interno no servidor: {ex.Message}"));
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin,moderator,artist")]
    public async Task<IActionResult> EditWork(EditarWorkViewModel EditWork,int id) 
    {

        // pegar id do usuario autenticado
        var userIdClaim = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value);


        // verificar se usuario tem permisao para editar
        var work = await _context.Works.FirstOrDefaultAsync(w => w.WorkId == id);
        if(work == null)
        {
            return BadRequest("obra nao encontrada!");
        }
        
        if (work.ArtistUserID != userIdClaim) 
        {
            return Unauthorized("Você não tem permissão para editar essa obra.");
        }

        // 

        try
        {   
            work.NameWork = EditWork.NameWork;
            work.Description = EditWork.Description;
            work.ImageUrl = EditWork.ImageUrl;
            work.IsAvailable = EditWork.IsAvailable;
            work.LastUpdateDate = DateTime.UtcNow;

            await _WorkRepository.PutAsync(work);
            return Ok(new ResultViewModels<WorkModel>(work));
        }
        catch (MySqlException bd)
        {
            return StatusCode(500, new ResultViewModels<string>($"Erro ao acessar o banco de dados: {bd.Message}"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModels<string>($"Erro interno no servidor: {ex.Message}"));
        }
    }


    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin,moderator,artist")]
    public async Task<IActionResult> DeleteWork(int id)
    {
        var userIdClaim = int.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value);

        var work = await _context.Works
               .AsNoTracking()
               .FirstOrDefaultAsync(w => w.WorkId == id);

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
            await _WorkRepository.DeleteAsync(work);

            return Ok(new ResultViewModels<string>("Work apagado!"));
        }
        catch (MySqlException bd)
        {
            return StatusCode(500, new ResultViewModels<string>($"Erro ao acessar o banco de dados: {bd.Message}"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModels<string>($"Erro interno no servidor: {ex.Message}"));
        }
    }

}
