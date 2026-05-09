using Backend.BusinessLogic.DTOs.Cart;
using Backend.BusinessLogic.DTOs.Products;
using Backend.BusinessLogic.Services.Interfaces;
using Backend.DataAccess.Entities;
using Backend.DataAccess.Entities.Enums;
using Backend.DataAccess.Repositories.Interfaces;

namespace Backend.BusinessLogic.Services;

public class CartService(
    IRepository<Cart> cartRepository,
    IRepository<Product> productRepository,
    IRepository<Promotion> promotionRepository) : ICartService
{
    public async Task<CartSummaryDto> GetCartAsync()
    {
        var items = await cartRepository.GetAllAsync();
        var activePromotions = (await promotionRepository.GetAllAsync())
            .Where(p => p.IsActive)
            .ToList();

        var cartItemsDto = MapToCartItemDtos(items);
        var subtotal = cartItemsDto.Sum(i => i.LineTotal);

        var totalDiscount = CalculateItemDiscounts(items, activePromotions);
        totalDiscount += CalculateCartWideDiscounts(items, activePromotions, subtotal);

        return new CartSummaryDto
        {
            Items = cartItemsDto,
            Subtotal = subtotal,
            TotalDiscount = totalDiscount,
            FinalPrice = subtotal - totalDiscount
        };
    }

    public async Task<CartGetDto> AddItemToCartAsync(CartCreateDto cartCreateDto)
    {
        if (cartCreateDto.Quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(cartCreateDto.Quantity),
                "Quantity must be greater than zero.");

        // Ensure product exists before adding a cart line.
        await productRepository.GetByIdAsync(cartCreateDto.ProductId);

        var existingItem = (await cartRepository.GetAllAsync())
            .FirstOrDefault(item => item.ProductId == cartCreateDto.ProductId);

        if (existingItem is not null)
        {
            existingItem.Quantity += cartCreateDto.Quantity;
            var updatedItem = await cartRepository.UpdateAsync(existingItem);
            return MapToDto(updatedItem);
        }

        var newItem = new Cart
        {
            ProductId = cartCreateDto.ProductId,
            Quantity = cartCreateDto.Quantity
        };

        var createdItem = await cartRepository.AddAsync(newItem);
        return MapToDto(createdItem);
    }

    public async Task<CartGetDto> UpdateCartItemQuantityAsync(int itemId, CartUpdateDto cartUpdateDto)
    {
        if (cartUpdateDto.Quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(cartUpdateDto.Quantity),
                "Quantity must be greater than zero.");

        var existingItem = await cartRepository.GetByIdAsync(itemId)
                           ?? throw new KeyNotFoundException($"Cart item with ID {itemId} not found.");

        existingItem.Quantity = cartUpdateDto.Quantity;

        var updatedItem = await cartRepository.UpdateAsync(existingItem);
        return MapToDto(updatedItem);
    }

    public async Task DeleteCartItemAsync(int itemId)
    {
        var existingItem = await cartRepository.GetByIdAsync(itemId)
                           ?? throw new KeyNotFoundException($"Cart item with ID {itemId} not found.");

        await cartRepository.DeleteAsync(existingItem);
    }

    public async Task DeleteCartAsync()
    {
        var items = await cartRepository.GetAllAsync();

        foreach (var item in items)
            await cartRepository.DeleteAsync(item);
    }

    private static List<CartItemDto> MapToCartItemDtos(List<Cart> items)
    {
        return items.Select(item => new CartItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId,
            ProductName = item.Product.Name,
            Description = item.Product.Description ?? string.Empty,
            ImageUrl = item.Product.ImageUrl ?? string.Empty,
            Quantity = item.Quantity,
            UnitPrice = item.Product.Price,
            LineTotal = item.Quantity * item.Product.Price
        }).ToList();
    }

    private decimal CalculateItemDiscounts(List<Cart> items, List<Promotion> activePromotions)
    {
        var totalDiscount = 0m;

        foreach (var item in items)
        {
            var itemSubtotal = item.Quantity * item.Product.Price;

            // Quantity-triggered promotions for this product/category
            var quantityPromos = activePromotions.Where(p =>
                IsApplicableToItem(p, item) &&
                p.Type == PromotionType.Quantity &&
                item.Quantity >= p.Threshold);

            totalDiscount += quantityPromos.Sum(promo =>
                CalculateDiscount(promo, item.Quantity, item.Product.Price));

            // CartTotal-triggered promotions scoped to this product/category
            var scopedTotalPromos = activePromotions.Where(p =>
                IsApplicableToItem(p, item) &&
                p.Type == PromotionType.CartTotal &&
                itemSubtotal >= p.Threshold);

            totalDiscount += scopedTotalPromos.Sum(promo =>
                CalculateDiscount(promo, item.Quantity, item.Product.Price, itemSubtotal));
        }

        return totalDiscount;
    }

    private decimal CalculateCartWideDiscounts(List<Cart> items, List<Promotion> activePromotions, decimal subtotal)
    {
        var totalQuantity = items.Sum(i => i.Quantity);
        var cartWidePromos = activePromotions.Where(p =>
            p.ProductId == null && p.CategoryId == null &&
            ((p.Type == PromotionType.CartTotal && subtotal >= p.Threshold) ||
             (p.Type == PromotionType.Quantity && totalQuantity >= p.Threshold)));

        return cartWidePromos.Sum(promo => CalculateDiscount(promo, totalQuantity, 0, subtotal));
    }

    private static bool IsApplicableToItem(Promotion promo, Cart item)
    {
        return promo.ProductId == item.ProductId ||
               (promo.CategoryId != null && item.Product.Categories.Any(c => c.Id == promo.CategoryId));
    }

    private static decimal CalculateDiscount(Promotion promo, int quantity, decimal unitPrice, decimal totalAmount = 0)
    {
        if (promo.Reward == PromotionReward.PercentDiscount)
        {
            var baseAmount = promo.Type == PromotionType.CartTotal ? totalAmount : quantity * unitPrice;
            return baseAmount * (promo.RewardValue / 100m);
        }

        if (promo.Reward == PromotionReward.FreeItems)
            // For free items, we assume the discount is equivalent to the price of 'RewardValue' items
            // This usually applies to Quantity based promos
            return promo.RewardValue * unitPrice;

        return 0;
    }

    private static CartGetDto MapToDto(Cart cart)
    {
        return new CartGetDto
        {
            Id = cart.Id,
            Product = new ProductGetDto
            {
                Id = cart.Product.Id,
                Name = cart.Product.Name,
                Description = cart.Product.Description ?? string.Empty,
                Price = cart.Product.Price,
                ImageUrl = cart.Product.ImageUrl
            },
            Quantity = cart.Quantity
        };
    }
}