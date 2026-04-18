namespace SmartShoppingAssistantBackend.DataAccess.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }

    // Navigation properties
    public ICollection<ProductCategory> ProductCategories { get; set; } = [];
    public ICollection<Promotion> Promotions { get; set; } = [];
    public ICollection<CartItem> CartItems { get; set; } = [];
}
