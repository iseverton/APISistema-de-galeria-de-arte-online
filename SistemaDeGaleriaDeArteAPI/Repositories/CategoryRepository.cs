using Microsoft.EntityFrameworkCore;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Interfaces;
using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.ViewModels;

namespace SistemaDeGaleriaDeArteAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryModel>> GetAsync()
        {
            var categories = await _context.Categories
                 .AsNoTracking()
                 .ToListAsync();

            return categories;
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoryID == id);

            return category;
        }

        public async Task<CategoryModel> PostAsync(CategoryViewModel model)
        {
            var category = new CategoryModel
            (
               model.CategoryName,
               model.Description  
            );

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryModel> PutAsync(CategoryModel model)
        {
            _context.Categories.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<CategoryModel> DeleteAsync(CategoryModel model)
        {
            _context.Categories.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

    }
}
