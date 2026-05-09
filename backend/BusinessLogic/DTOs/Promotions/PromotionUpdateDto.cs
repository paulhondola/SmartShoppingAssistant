using Backend.DataAccess.Entities.Enums;

namespace Backend.BusinessLogic.DTOs.Promotions;

public class PromotionUpdateDto
{
    public string? Name { get; set; }
    public PromotionType? Type { get; set; }
    public decimal? Threshold { get; set; }
    public PromotionReward? Reward { get; set; }
    public int? RewardValue { get; set; }
    public int? ProductId { get; set; }
    public int? CategoryId { get; set; }
    public bool? IsActive { get; set; }
}
