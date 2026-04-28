namespace SmartShoppingAssistantBackend.BusinessLogic.DTOs.Products;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public List<int> CategoryIds { get; set; } = [];
    public List<int> PromotionIds { get; set; } = [];
}