using Backend.BusinessLogic.DTOs.Promotions;
using Backend.BusinessLogic.Services.Interfaces;

namespace Backend.BusinessLogic.Tools;

public class ShoppingTools
{
    public static async Task<List<PromotionGetDto>> GetPromotionsForProductAsync(int productId,
        IPromotionService promotionService)
    {
        return await promotionService.GetForProductAsync(productId);
    }
}