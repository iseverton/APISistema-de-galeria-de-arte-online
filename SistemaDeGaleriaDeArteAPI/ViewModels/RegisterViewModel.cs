using SistemaDeGaleriaDeArteAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeGaleriaDeArteAPI.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O campo nome deve ser preenchido")]
    [StringLength(120, MinimumLength = 5, ErrorMessage = "o campo nome deve ter entre 5 a 120 caracteres")]
    public string Name { get; set; } = string.Empty;


    [Required(ErrorMessage = "O campo de nome deve ser preenchido")]
    [StringLength(120, MinimumLength = 15, ErrorMessage = "o campo email deve ter entre 15 a 120 caracteres")]
    [EmailAddress(ErrorMessage = "Formato do email invalido")]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "O campo de email  deve ser preenchido")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "o campo email deve ter 11 caracteres")]
    //[RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = "O número de telefone deve estar no formato (XX) XXXXX-XXXX.")]
    public string Phone { get; set; } = string.Empty;


    public string Bio { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo de Passaword deve ser preenchido")]
    [StringLength(120, MinimumLength = 5, ErrorMessage = "o campo senha deve ter entre 5 a 120 caracteres")]
    public string Password { get; set; } = string.Empty;


    [Required(ErrorMessage = "O campo de confirmar senha deve ser preenchido")]
    [Compare("Password",ErrorMessage = "As duas senha deves ser iguais")]
    public string ConfirmerPass { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo de role deve ser preenchido")]
    public RolesEnum  Role { get; set; }


}
