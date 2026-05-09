using Logic.Services.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;
using Logic.DTOs.Promotions;

namespace Logic.Services;

public class PromotionService(IPromotionRepository promotionRepository) : IPromotionService
{
    public async Task<List<PromotionGetDto>> GetAllAsync()
    {
        var promotions = await promotionRepository.GetAllAsync();
        return promotions.Select(MapToDto).ToList();
    }

    public async Task<PromotionGetDto> GetByIdAsync(int id)
    {
        var promotion = await promotionRepository.GetByIdAsync(id);
        return MapToDto(promotion);
    }

    public async Task<PromotionGetDto> CreateAsync(PromotionCreateDto dto)
    {
        var promotion = new Promotion
        {
            Name = dto.Name,
            Type = dto.Type,
            Threshold = dto.Threshold,
            Reward = dto.Reward,
            RewardValue = dto.RewardValue,
            ProductId = dto.ProductId,
            CategoryId = dto.CategoryId,
            IsActive = dto.IsActive,
        };
        var created = await promotionRepository.AddAsync(promotion);
        return MapToDto(created);
    }

    public async Task<PromotionGetDto> UpdateAsync(int id, PromotionUpdateDto dto)
    {
        var promotion = await promotionRepository.GetByIdAsync(id);
        promotion.Name = dto.Name;
        promotion.Type = dto.Type;
        promotion.Threshold = dto.Threshold;
        promotion.Reward = dto.Reward;
        promotion.RewardValue = dto.RewardValue;
        promotion.ProductId = dto.ProductId;
        promotion.CategoryId = dto.CategoryId;
        promotion.IsActive = dto.IsActive;
        var updated = await promotionRepository.UpdateAsync(promotion);
        return MapToDto(updated);
    }

    public Task DeleteAsync(int id) => promotionRepository.DeleteAsync(id);

    public async Task<List<PromotionGetDto>> GetForProductAsync(int productId)
    {
        var promotions = await promotionRepository.GetForProductAsync(productId);
        return promotions.Select(MapToDto).ToList();
    }

    private static PromotionGetDto MapToDto(Promotion p) =>
        new()
        {
            Id = p.Id,
            Name = p.Name,
            Type = p.Type,
            Threshold = p.Threshold,
            Reward = p.Reward,
            RewardValue = p.RewardValue,
            ProductId = p.ProductId,
            CategoryId = p.CategoryId,
            IsActive = p.IsActive,
        };
}
