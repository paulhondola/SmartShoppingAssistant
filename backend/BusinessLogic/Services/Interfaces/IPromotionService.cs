using Backend.BusinessLogic.DTOs.Promotions;

namespace Backend.BusinessLogic.Services.Interfaces;

public interface IPromotionService
{
    Task<PromotionGetDto> GetPromotionByIdAsync(int id);
    Task<List<PromotionGetDto>> GetAllPromotionsAsync();
    Task<PromotionGetDto> AddPromotionAsync(PromotionCreateDto promotionCreateDto);
    Task<PromotionGetDto> UpdatePromotionAsync(int id, PromotionUpdateDto promotionUpdateDto);
    Task DeletePromotionAsync(int id);
}
