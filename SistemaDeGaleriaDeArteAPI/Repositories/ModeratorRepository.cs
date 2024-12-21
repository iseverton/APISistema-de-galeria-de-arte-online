using Microsoft.EntityFrameworkCore;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Interfaces;
using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Repositories
{
    public class ModeratorRepository: IModeratorRepository
    {
        private readonly AppDbContext _context;

        public ModeratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> DeletarUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<WorkModel> DeletarWorkAsync(int id)
        {
            var work = await _context.Works.FirstOrDefaultAsync(w => w.WorkId == id);
            _context.Works.Remove(work);
            await _context.SaveChangesAsync();
            return work;
        }
    }
}
