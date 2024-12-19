namespace SistemaDeGaleriaDeArteAPI.ViewModels
{
    public class WorkViewModel
    {
        public string NameWork { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        public int ArtistId { get; set; }
    }
}
