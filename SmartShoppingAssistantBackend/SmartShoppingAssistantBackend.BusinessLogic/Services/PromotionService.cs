using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Promotions;
using SmartShoppingAssistantBackend.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistantBackend.DataAccess.Entities;
using SmartShoppingAssistantBackend.DataAccess.Repositories;

namespace SmartShoppingAssistantBackend.BusinessLogic.Services;

public class PromotionService(IRepository<Promotion> promotionRepository) : IPromotionService
{
    public async Task<PromotionGetDto> GetPromotionByIdAsync(int id)
    {
        var promotion = await promotionRepository.GetByIdAsync(id)
                        ?? throw new KeyNotFoundException($"Promotion with ID {id} not found.");

        return MapToDto(promotion);
    }

    public async Task<List<PromotionGetDto>> GetAllPromotionsAsync()
    {
        var promotions = await promotionRepository.GetAllAsync();
        return promotions.Select(MapToDto).ToList();
    }

    public async Task<PromotionGetDto> AddPromotionAsync(PromotionCreateDto promotionCreateDto)
    {
        var promotion = new Promotion
        {
            Name = promotionCreateDto.Name,
            Type = promotionCreateDto.Type,
            Threshold = promotionCreateDto.Threshold,
            Reward = promotionCreateDto.Reward,
            RewardValue = promotionCreateDto.RewardValue,
            ProductId = promotionCreateDto.ProductId,
            CategoryId = promotionCreateDto.CategoryId,
            IsActive = promotionCreateDto.IsActive
        };

        var added = await promotionRepository.AddAsync(promotion);
        return MapToDto(added);
    }

    public async Task<PromotionGetDto> UpdatePromotionAsync(int id, PromotionUpdateDto promotionUpdateDto)
    {
        var promotion = await promotionRepository.GetByIdAsync(id)
                        ?? throw new KeyNotFoundException($"Promotion with ID {id} not found.");

        if (promotionUpdateDto.Name is not null)
            promotion.Name = promotionUpdateDto.Name;

        if (promotionUpdateDto.Type is not null)
            promotion.Type = promotionUpdateDto.Type.Value;

        if (promotionUpdateDto.Threshold is not null)
            promotion.Threshold = promotionUpdateDto.Threshold.Value;

        if (promotionUpdateDto.Reward is not null)
            promotion.Reward = promotionUpdateDto.Reward.Value;

        if (promotionUpdateDto.RewardValue is not null)
            promotion.RewardValue = promotionUpdateDto.RewardValue.Value;

        if (promotionUpdateDto.ProductId is not null)
            promotion.ProductId = promotionUpdateDto.ProductId.Value;

        if (promotionUpdateDto.CategoryId is not null)
            promotion.CategoryId = promotionUpdateDto.CategoryId.Value;

        if (promotionUpdateDto.IsActive is not null)
            promotion.IsActive = promotionUpdateDto.IsActive.Value;

        var updated = await promotionRepository.UpdateAsync(promotion);
        return MapToDto(updated);
    }

    public async Task DeletePromotionAsync(int id)
    {
        var promotion = await promotionRepository.GetByIdAsync(id)
                        ?? throw new KeyNotFoundException($"Promotion with ID {id} not found.");

        await promotionRepository.DeleteAsync(promotion);
    }

    private static PromotionGetDto MapToDto(Promotion promotion) =>
        new()
        {
            Id = promotion.Id,
            Name = promotion.Name,
            Type = promotion.Type,
            Threshold = promotion.Threshold,
            Reward = promotion.Reward,
            RewardValue = promotion.RewardValue,
            IsActive = promotion.IsActive,
            ProductName = promotion.Product?.Name,
            CategoryName = promotion.Category?.Name
        };
}
