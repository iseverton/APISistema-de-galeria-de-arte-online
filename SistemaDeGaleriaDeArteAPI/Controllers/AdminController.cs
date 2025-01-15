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

namespace SistemaDeGaleriaDeArteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AdminController(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("create")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> RegisterAdminOrModerator(RegisterViewModel model)
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

            return Ok(new ResultViewModels<UserModel>(user));
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
