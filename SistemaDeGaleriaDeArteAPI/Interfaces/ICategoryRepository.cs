using SistemaDeGaleriaDeArteAPI.Models;
using SistemaDeGaleriaDeArteAPI.ViewModels;

namespace SistemaDeGaleriaDeArteAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAsync();
        Task<CategoryModel> GetByIdAsync(int id);
        Task<CategoryModel> PostAsync(CategoryViewModel model);
        Task<CategoryModel> PutAsync(CategoryModel model);
        Task<CategoryModel> DeleteAsync(CategoryModel model);
    }
}
