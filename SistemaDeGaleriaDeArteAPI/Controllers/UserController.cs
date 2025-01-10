using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Interfaces;
using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.Repositories;
using SistemaDeGaleriaDeArteAPI.Services;
using SistemaDeGaleriaDeArteAPI.ViewModels;

namespace SistemaDeGaleriaDeArteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public UserController(AppDbContext context, TokenService tokenService, IUserRepository userRepository)
    {
        _context = context;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    [HttpGet("artists")]
    public async Task<ActionResult<List<UserModel>>> GetAllArtist()
    {
        try
        {
            var artists = await _userRepository.GetArtistsAsync();

            if (artists is null)
            {
                NotFound("nenhum artista foi encontrada");
            }
            return Ok(new ResultViewModels<List<UserModel>>(artists));
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

    [HttpGet("artists/{id:int}")]
    public async Task<ActionResult<UserModel>> GetArtistById(int id)
    {
        try
        {
            var artist = await _userRepository.GetArtistByIdAsync(id);


            if (artist is null)
            {
                NotFound("nenhum artista foi encontrada");
            }
            return Ok(new ResultViewModels<UserModel>(artist));
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

    [HttpGet("works")]
    public async Task<ActionResult<List<WorkModel>>> GetWorkAll()
    {
        try
        {
            var works = await _userRepository.GetworksAsync();
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
            return StatusCode(500, new ResultViewModels<string>($"Erro interno no servidor: {ex.Message}"));
        }
    }

    [HttpGet("works/{id:int}")]
    public async Task<ActionResult<List<WorkModel>>> GetWorkById(int id)
    {
        try
        {
            var work = await _userRepository.GetWorkByIdAsync(id);
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

    [HttpGet("category")]
    public async Task<IActionResult> GetAllCategory()
    {
        try
        {
            var categories = await _userRepository.GetCategoriesAsync();
            return Ok(new ResultViewModels<List<CategoryModel>>(categories));
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

    [HttpGet("category/{id:int}")]
    public async Task<IActionResult> GetcategoryById(int id)
    {
        try
        {
            var category = await _userRepository.GetCategoryByIdAsync(id);
            return Ok(new ResultViewModels<CategoryModel>(category));
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