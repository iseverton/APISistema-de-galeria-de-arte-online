using SistemaDeGaleriaDeArteAPI.Enums;

namespace SistemaDeGaleriaDeArteAPI.Models;

public class UserModel
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public string? Bio { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public RolesEnum Role { get; set; }
    public List<WorkModel>? Works { get; set; }


    public UserModel() { }

    public UserModel(
         string userName, string userEmail, string password,
        string? bio,string? phoneNumber,RolesEnum role)
    {
        UserName = userName;
        UserEmail = userEmail;
        Password = password;
        Bio = bio;
        CreatedAt = DateTime.UtcNow;
        Role = role;

        PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber;
    }
}
