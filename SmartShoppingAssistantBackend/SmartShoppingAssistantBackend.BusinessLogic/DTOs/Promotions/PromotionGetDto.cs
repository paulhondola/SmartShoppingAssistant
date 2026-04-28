using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Categories;
using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Products;
using SmartShoppingAssistantBackend.DataAccess.Entities.Enums;

namespace SmartShoppingAssistantBackend.BusinessLogic.DTOs.Promotions;

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
