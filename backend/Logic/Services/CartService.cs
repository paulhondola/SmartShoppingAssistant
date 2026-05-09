using Data.Entities;
using Data.Entities.Enums;
using Data.Repositories.Interfaces;
using Logic.DTOs.Cart;
using Logic.DTOs.Promotions;
using Logic.Services.Interfaces;

namespace Logic.Services;

public class CartService(
    ICartRepository cartItemRepository,
    IProductRepository productRepository,
    IPromotionService promotionService
) : ICartService
{
    public async Task<CartGetDto> GetCartAsync()
    {
        var items = await cartItemRepository.GetAllWithProductAsync();
        return await BuildCartDtoAsync(items);
    }

    public async Task<CartItemGetDto> AddItemAsync(AddCartItemDto dto)
    {
        await productRepository.GetByIdAsync(dto.ProductId);

        var existing = await cartItemRepository.GetByProductIdAsync(dto.ProductId);
        if (existing != null)
        {
            existing.Quantity += dto.Quantity;
            await cartItemRepository.UpdateAsync(existing);
            return MapToItemDto(existing);
        }

        var item = new Cart { ProductId = dto.ProductId, Quantity = dto.Quantity };
        await cartItemRepository.AddAsync(item);
        var added = await cartItemRepository.GetByIdWithProductAsync(item.Id);
        return MapToItemDto(added);
    }

    public async Task<CartItemGetDto> UpdateItemAsync(int itemId, UpdateCartItemDto dto)
    {
        var item = await cartItemRepository.GetByIdWithProductAsync(itemId);
        item.Quantity = dto.Quantity;
        await cartItemRepository.UpdateAsync(item);
        return MapToItemDto(item);
    }

    public Task RemoveItemAsync(int itemId) => cartItemRepository.DeleteAsync(itemId);

    public Task ClearCartAsync() => cartItemRepository.ClearAsync();

    private async Task<CartGetDto> BuildCartDtoAsync(IList<Cart> items)
    {
        var itemDtos = items.Select(MapToItemDto).ToList();
        var subtotal = itemDtos.Sum(i => i.Subtotal);

        var applied = new List<AppliedPromotionDto>();
        var seenPromotionIds = new HashSet<int>();

        // Product/category-scoped promotions
        foreach (var cartItem in items)
        {
            var promos = await promotionService.GetForProductAsync(cartItem.ProductId);
            var itemSubtotal = cartItem.Product.Price * cartItem.Quantity;

            foreach (var promo in promos)
            {
                if (!seenPromotionIds.Add(promo.Id))
                    continue;

                var discount = CalculateDiscount(promo, cartItem.Quantity, itemSubtotal, subtotal);
                if (discount < 0)
                    applied.Add(new AppliedPromotionDto(promo.Name, discount));
            }
        }

        // Cart-wide promotions (no product, no category)
        var cartWidePromos = await promotionService.GetAllActiveAsync();
        foreach (var promo in cartWidePromos.Where(p => p.ProductId == null && p.CategoryId == null))
        {
            if (!seenPromotionIds.Add(promo.Id))
                continue;

            var discount = CalculateDiscount(promo, 0, subtotal, subtotal);
            if (discount < 0)
                applied.Add(new AppliedPromotionDto(promo.Name, discount));
        }

        var totalDiscount = applied.Sum(a => a.Discount);
        return new CartGetDto
        {
            Items = itemDtos,
            Subtotal = subtotal,
            AppliedPromotions = applied,
            TotalDiscount = totalDiscount,
            Total = subtotal + totalDiscount,
        };
    }

    private static decimal CalculateDiscount(
        PromotionGetDto promo,
        int quantity,
        decimal itemSubtotal,
        decimal cartSubtotal
    )
    {
        return promo.Type switch
        {
            PromotionType.Quantity when quantity >= (int)promo.Threshold =>
                promo.Reward switch
                {
                    PromotionReward.FreeItems =>
                        -(itemSubtotal / quantity * promo.RewardValue),
                    PromotionReward.PercentDiscount =>
                        -(itemSubtotal * promo.RewardValue / 100m),
                    _ => 0m,
                },
            PromotionType.CartTotal when cartSubtotal >= promo.Threshold =>
                promo.Reward switch
                {
                    PromotionReward.PercentDiscount =>
                        -(cartSubtotal * promo.RewardValue / 100m),
                    _ => 0m,
                },
            _ => 0m,
        };
    }

    private static CartItemGetDto MapToItemDto(Cart ci) =>
        new()
        {
            Id = ci.Id,
            ProductId = ci.ProductId,
            ProductName = ci.Product.Name,
            UnitPrice = ci.Product.Price,
            Quantity = ci.Quantity,
            Subtotal = ci.Product.Price * ci.Quantity,
        };
}
