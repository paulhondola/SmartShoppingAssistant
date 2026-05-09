namespace Backend.DataAccess.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<Promotion> Promotions { get; set; } = [];
}