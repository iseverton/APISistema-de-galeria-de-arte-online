using System.ComponentModel.DataAnnotations;

namespace SistemaDeGaleriaDeArteAPI.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "O campo nome deve ser preenchido")]
        [StringLength(120, MinimumLength = 5, ErrorMessage = "o campo nome deve ter entre 5 a 120 caracteres")]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo descrição deve ser preenchido")]
        public string Description { get; set; } = string.Empty;
    }
}
