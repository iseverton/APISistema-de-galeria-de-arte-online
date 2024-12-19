namespace SistemaDeGaleriaDeArteAPI.Models;

public class WorkModel
{
    public int WorkId { get; set; }
    public string NameWork { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public bool IsAvailable { get; set; }


    public WorkModel() { }
    public WorkModel(string namework, string description, string imageUrl, 
        DateTime createdat, DateTime lastUpdateDate,bool isAvailable,   int categoryId,int artistId)
    {
        NameWork = namework;
        Description = description;
        ImageUrl = imageUrl;
        CreatedAt = DateTime.UtcNow;
        LastUpdateDate = DateTime.UtcNow;
        IsAvailable = isAvailable;
        CategoryID = categoryId;
        ArtistUserID = artistId;

    }
    public int CategoryID { get; set; }
    public int ArtistUserID { get; set; }
    public CategoryModel Category { get; set; }
    public UserModel Artist { get; set; }
}
