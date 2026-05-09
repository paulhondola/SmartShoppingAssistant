using Backend.BusinessLogic.DTOs.Products;

namespace Backend.BusinessLogic.DTOs.Categories;

public class CategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ProductSummaryDto> Products { get; set; } = [];
}
