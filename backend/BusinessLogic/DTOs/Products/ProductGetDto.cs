using Backend.BusinessLogic.DTOs.Categories;
using Backend.BusinessLogic.DTOs.Promotions;

namespace Backend.BusinessLogic.DTOs.Products;

public class ProductGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public List<CategorySummaryDto> Categories { get; set; } = [];
    public List<PromotionSummaryDto> Promotions { get; set; } = [];
}