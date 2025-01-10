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
    private readonly ILogger<AccountController> _logger;

    public AccountController(AppDbContext context, TokenService tokenService, ILogger<AccountController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model) 
    {
         
        // Se a pessoa que esta cadastrando for um artista deve fornecer o seu telefone
        if (  model.Role.ToString().Equals("artist", StringComparison.InvariantCultureIgnoreCase)  && string.IsNullOrEmpty(model.Phone))
        {
            return BadRequest(new ResultViewModels<string>($"Os artistas devem fornecer o seu telefone para os usuarios entrar em contato!"));
        }

        if (!string.IsNullOrWhiteSpace(model.Phone))
        {
            var phone = await _context.Users.FirstOrDefaultAsync(p => p.PhoneNumber == model.Phone);
            if (phone != null)
            {
                return BadRequest(new ResultViewModels<string>($"Esse telefone já está sendo usado!"));
            }
        }


        // Verificar se email ou telefone ja existem
        var email = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == model.Email);

        if (email != null) {
            return BadRequest(new ResultViewModels<string>($"Esse email ja existe,faca o login!"));
        }

        
        // Restrigir acesso aos roles para usuario comum
        if (model.Role.ToString().ToLower() != "user" && model.Role.ToString().ToLower() != "artist")
        {
            return BadRequest(new ResultViewModels<string>($"O (role) deve ser 'user' ou 'artista'!"));
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
            _logger.LogError(bd, "Erro ao acessar o banco de dados! ");
            return StatusCode(500, new ResultViewModels<string>($"Erro ao acessar o banco de dados: {bd.Message}"));
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Erro interno no servidor! ");
            return StatusCode(500, new ResultViewModels<string>("Erro interno no servidor"));
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model) 
    {
        _logger.LogInformation("Acessou o login de usuario");
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
