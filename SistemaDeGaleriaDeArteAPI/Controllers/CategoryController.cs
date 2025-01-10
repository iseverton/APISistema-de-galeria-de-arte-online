using Microsoft.AspNetCore.Authorization;
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
public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(AppDbContext context, ICategoryRepository categoryRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() 
    {
        try
        {
          var categories =  await _categoryRepository.GetAsync();
            if (categories == null) {
                return NotFound("nehuma categoria foi encontrada!");
            }
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) 
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
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
    
    [HttpPost]
    [Authorize(Roles = "admin,moderator")]
    public async Task<IActionResult> CreateCategory(CategoryViewModel model)
    {
        if (model == null)
        {
            BadRequest("Preencha todos os campo!");
        }

        try
        {
            var category = await _categoryRepository.PostAsync(model);
            if (category == null) {
                return BadRequest("categoria nao pode ser vazia");
            }
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

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin,moderator")]
    public async Task<IActionResult> Put(CategoryViewModel model, int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
        if (category == null)
        {
            BadRequest("categoria não encontrada!");
        }

        try
        {
            category.CategoryName = model.CategoryName;
            category.Description = model.Description;
            await _categoryRepository.PutAsync(category);
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

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin,moderator")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
        if (category == null)
        {
            BadRequest("categoria não encontrada!");
        }
        try
        {
            await _categoryRepository.DeleteAsync(category);
            return Ok(new ResultViewModels<String>("categoria deletada!"));
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
