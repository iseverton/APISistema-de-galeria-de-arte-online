namespace SistemaDeGaleriaDeArteAPI.Models;

public class CategoryModel
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public CategoryModel()
    {
    }

    public CategoryModel(string categoryname, string description)
    {
        CategoryName = categoryname;
        Description = description;
    }

    public List<WorkModel>? Works { get; set; }
}
