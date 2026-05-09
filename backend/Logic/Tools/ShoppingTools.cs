using System.ComponentModel;
using Logic.DTOs.Promotions;
using Logic.Services.Interfaces;

namespace Logic.Tools;

public static class ShoppingTools
{
    [Description(
        "Get all active promotions that apply to a specific product (by product ID or its category)."
    )]
    public static async Task<List<PromotionGetDto>> GetPromotionsForProduct(
        [Description("The product ID to check")] int productId,
        IPromotionService promotionService
    )
    {
        return await promotionService.GetForProductAsync(productId);
    }
}
