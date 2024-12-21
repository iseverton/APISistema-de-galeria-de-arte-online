using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Interfaces
{
    public interface IModeratorRepository
    {
        Task<WorkModel> DeletarWorkAsync(int id);
        Task<UserModel> DeletarUserAsync(int id);
    }
}
