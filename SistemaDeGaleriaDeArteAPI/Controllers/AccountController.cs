using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MySqlConnector;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.Services;
using SistemaDeGaleriaDeArteAPI.ViewModels;

namespace SistemaDeGaleriaDeArteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public AccountController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model) 
    {
        // Se a pessoa que esta cadastrando for um artista deve fornecer o seu telefone
        if (model.Role.ToString().ToLower() == "artist" & string.IsNullOrEmpty(model.Phone))
        {
            return BadRequest("Os artistas devem fornecer o seu telefone para os usuarios entrar em contato!");
        }

        if (!string.IsNullOrWhiteSpace(model.Phone))
        {
            var phone = await _context.Users.FirstOrDefaultAsync(p => p.PhoneNumber == model.Phone);
            if (phone != null)
            {
                return BadRequest($"Esse telefone já está sendo usado! {phone}");
            }
        }


        // Verificar se email ou telefone ja existem
        var email = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == model.Email);

        if (email != null) {
            return BadRequest("Esse email ja existe,faca o login!");
        }

        
        // Restrigir acesso aos roles para usuario comum
        if (model.Role.ToString().ToLower() != "user" && model.Role.ToString().ToLower() != "artist")
        {
            return BadRequest("O (role) deve ser 'user' ou 'artista'.");
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
        catch(Exception ex) 
        {
            return BadRequest($"Falha interna! {ex.Message} e {ex.InnerException}");
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model) 
    {
        // Confirmar email e senha
        var email = await _context.Users.FirstOrDefaultAsync(e=> e.UserEmail == model.Email);
        var senha = await _context.Users.FirstOrDefaultAsync(s => s.Password == model.Password);

        if(email == null || senha == null)
        {
            return BadRequest("Email ou senha errados");
        }

        try
        {
            var token = _tokenService.GerarToken(email);
            return Ok(token);
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
