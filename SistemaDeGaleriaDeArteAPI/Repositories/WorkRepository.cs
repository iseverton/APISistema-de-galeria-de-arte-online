using Microsoft.EntityFrameworkCore;
using SistemaDeGaleriaDeArteAPI.Data;
using SistemaDeGaleriaDeArteAPI.Interfaces;
using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.ViewModels;

namespace SistemaDeGaleriaDeArteAPI.Repositories
{
    public class WorkRepository : IWorkRepository
    {
        private readonly AppDbContext _context;

        public WorkRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkModel>> GetAsync()
        {
            var works = await _context.Works
                 .AsNoTracking()
                 .ToListAsync();
            return works;
        }


        public async Task<WorkModel> GetByIdAsync(int id)
        {
            var works = await _context.Works
               .AsNoTracking()
               .FirstOrDefaultAsync(w=> w.WorkId == id);
               
            return works;
        }


        public async Task<WorkModel> PostAsync(WorkViewModel model)
        {
            var work = new WorkModel(
                 model.NameWork,
                 model.Description,
                 model.ImageUrl,
                 DateTime.UtcNow,
                 DateTime.UtcNow,
                 model.IsAvailable,
                 model.CategoryId,
                 model.ArtistId
                );

            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();
            return work;
        }

        public async Task<WorkModel> PutAsync(WorkModel model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<WorkModel> DeleteAsync(WorkModel model)
        {
            _context.Works.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        

        

        

        
    }
}
