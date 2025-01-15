using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(UserModel user); 
    }
}
