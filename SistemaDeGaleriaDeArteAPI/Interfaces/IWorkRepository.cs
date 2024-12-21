using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.ViewModels;

namespace SistemaDeGaleriaDeArteAPI.Interfaces
{
    public interface IWorkRepository
    {
        Task<List<WorkModel>> GetAsync();
        Task<WorkModel> GetByIdAsync(int id);
        Task<WorkModel> PostAsync(WorkViewModel model);
        Task<WorkModel> PutAsync(WorkModel model);
        Task<WorkModel> DeleteAsync(WorkModel model);
    }
}
