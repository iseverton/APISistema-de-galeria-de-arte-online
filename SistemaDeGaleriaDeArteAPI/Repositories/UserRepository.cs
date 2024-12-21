using Microsoft.EntityFrameworkCore;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Interfaces;
using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Repositories;

public class UserRepository: IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    // artist
    public async Task<List<UserModel>> GetArtistsAsync()
    {
        var artists = await _context.Users
            .Where(u => ((int)u.Role) == 2)
            .AsNoTracking()
            .ToListAsync();
        return artists;
    }
    public async Task<UserModel> GetArtistByIdAsync(int id)
    {
        var artist = await _context.Users
          .FirstOrDefaultAsync(u => ((int)u.Role) == 2 && u.UserID == id);
        return artist;
    }

    // work
    public async Task<List<WorkModel>> GetworksAsync()
    {
        var works = await _context.Works
                .AsNoTracking()
                .ToListAsync();
        return works;
    }

    public async Task<WorkModel> GetWorkByIdAsync(int id)
    {
        var works = await _context.Works
               .AsNoTracking()
               .FirstOrDefaultAsync(w => w.WorkId == id);

        return works;
    }

    // category
    public async Task<List<CategoryModel>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
                 .AsNoTracking()
                 .ToListAsync();

        return categories;
    }

    public async Task<CategoryModel> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoryID == id);

        return category;
    }
}
