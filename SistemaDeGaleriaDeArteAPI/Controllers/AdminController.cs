using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.Services;
using SistemaDeGaleriaDeArteAPI.ViewModels;

namespace SistemaDeGaleriaDeArteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public AdminController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("criar")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> RegisterAdminAndModerator(RegisterViewModel model)
    {
        var email = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == model.Email);

        if (email != null)
        {
            return BadRequest("Esse email ja existe,faca o login");
        }

        try
        {
            var user = new UserModel
                (
                model.Name,
                model.Email,
                model.Password,
                model.Bio,
                model.Phone,
                model.Role
                );

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
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
