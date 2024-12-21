using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Interfaces
{
    public interface IUserRepository
    {
        // artist
        Task<List<UserModel>> GetArtistsAsync();
        Task<UserModel> GetArtistByIdAsync(int id);

        // work
        Task<List<WorkModel>> GetworksAsync();
        Task<WorkModel> GetWorkByIdAsync(int id);

        // category
        Task<List<CategoryModel>> GetCategoriesAsync();
        Task<CategoryModel> GetCategoryByIdAsync(int id);
    }
}
