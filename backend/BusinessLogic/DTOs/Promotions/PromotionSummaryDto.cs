using Backend.DataAccess.Entities.Enums;

namespace Backend.BusinessLogic.DTOs.Promotions;

public class PromotionSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PromotionReward Reward { get; set; }
    public int RewardValue { get; set; }
    public bool IsActive { get; set; }
}