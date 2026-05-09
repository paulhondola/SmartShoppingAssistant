using Backend.BusinessLogic.DTOs.Categories;
using Backend.BusinessLogic.DTOs.Products;
using Backend.DataAccess.Entities.Enums;

namespace Backend.BusinessLogic.DTOs.Promotions;

public class PromotionGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PromotionType Type { get; set; }
    public decimal Threshold { get; set; }
    public PromotionReward Reward { get; set; }
    public int RewardValue { get; set; }
    public bool IsActive { get; set; }
    public ProductSummaryDto? Product { get; set; }
    public CategorySummaryDto? Category { get; set; }
}
