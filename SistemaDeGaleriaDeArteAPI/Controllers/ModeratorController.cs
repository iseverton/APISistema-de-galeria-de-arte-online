using Microsoft.AspNetCore.Authorization;
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
public class ModeratorController : Controller
{
    private readonly AppDbContext _context;
    private readonly IModeratorRepository _moderatorRepository;
    

    public ModeratorController(AppDbContext context, IModeratorRepository moderatorRepository )
    {
        _context = context;
        _moderatorRepository = moderatorRepository;
    }

    // Deletar obra
    [HttpDelete("work/{id:int}")]
    [Authorize(Roles = "admin,moderator")]
    public async Task<IActionResult> DeleteWork(int id)
    {
        try
        {
            var work = await _moderatorRepository.DeletarWorkAsync(id);
            if (work == null)
            {
                return BadRequest("obra nao encontrada");
            }
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

    // Deletar user
    [HttpDelete("user/{id:int}")]
    [Authorize(Roles = "admin,moderator")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _moderatorRepository.DeletarUserAsync(id);
            if (user == null)
            {
                return BadRequest("user nao encontrada");
            }
            return Ok("user excluido!");
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
