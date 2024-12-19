namespace SistemaDeGaleriaDeArteAPI.ViewModels;

public class EditarWorkViewModel
{
    public string NameWork { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
