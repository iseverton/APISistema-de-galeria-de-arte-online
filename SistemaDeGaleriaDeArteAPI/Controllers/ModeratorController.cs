using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Services;
using System.Security.Claims;

namespace SistemaDeGaleriaDeArteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeratorController : Controller
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public ModeratorController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    // Deletar obra
    [HttpDelete("deletar/work/{id:int}")]
    [Authorize(Roles = "admin,moderador")]
    public async Task<IActionResult> DeleteWork(int id)
    {
        
        var work = await _context.Works.FirstOrDefaultAsync(w => w.WorkId == id);
        if (work == null)
        {
            return BadRequest("obra nao encontrada");
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

    // Deletar user
    [HttpDelete("deletar/user/{id:int}")]
    [Authorize(Roles = "admin,moderador")]
    public async Task<IActionResult> DeleteUser(int id)
    {

        var user = await _context.Users.FirstOrDefaultAsync(u=> u.UserID == id);
        if (user == null)
        {
            return BadRequest("user nao encontrada");
        }

        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

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
