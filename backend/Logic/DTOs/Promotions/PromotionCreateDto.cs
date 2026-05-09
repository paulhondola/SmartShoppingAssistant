using Data.Entities.Enums;

namespace Logic.DTOs.Promotions;

public class PromotionCreateDto
{
    public string Name { get; set; } = null!;
    public PromotionType Type { get; set; }
    public decimal Threshold { get; set; }
    public PromotionReward Reward { get; set; }
    public int RewardValue { get; set; }
    public int? ProductId { get; set; }
    public int? CategoryId { get; set; }
    public bool IsActive { get; set; }
}
