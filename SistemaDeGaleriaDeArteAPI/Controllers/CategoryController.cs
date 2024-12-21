using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Interfaces;
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
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha interna! {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) 
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha interna! {ex.Message}");
        }
    }
    
    [HttpPost("create")]
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
            return Ok(model);
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
            return Ok(category);
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
            return Ok("categoria deletada!");
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
