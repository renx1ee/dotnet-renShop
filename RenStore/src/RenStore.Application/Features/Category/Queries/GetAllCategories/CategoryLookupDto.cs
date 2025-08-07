namespace RenStore.Application.Features.Category.Queries.GetAllCategories;

public class CategoryLookupDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }

    public CategoryLookupDto(
        int id, 
        string name, 
        string description, 
        string imagePath)
    {
        Id = id;
        Name = name;
        Description = description;
        ImagePath = imagePath;
    }
}