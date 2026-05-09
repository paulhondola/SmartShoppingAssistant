using Logic.DTOs.Promotions;

namespace Logic.Services.Interfaces;

public interface IPromotionService
{
    Task<List<PromotionGetDto>> GetAllAsync();
    Task<PromotionGetDto> GetByIdAsync(int id);
    Task<PromotionGetDto> CreateAsync(PromotionCreateDto dto);
    Task<PromotionGetDto> UpdateAsync(int id, PromotionUpdateDto dto);
    Task DeleteAsync(int id);
    Task<List<PromotionGetDto>> GetForProductAsync(int productId);
}
