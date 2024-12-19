using System.ComponentModel.DataAnnotations;

namespace SistemaDeGaleriaDeArteAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "o campo email é obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "o campo email é obrigatorio")]
        public string Password { get; set; }
    }
}
