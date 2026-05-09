using Data.Entities;
using Data.Repositories.Interfaces;
using Logic.DTOs.Cart;
using Logic.Services.Interfaces;

namespace Logic.Services;

public class CartService(ICartRepository cartItemRepository, IProductRepository productRepository)
    : ICartService
{
    public async Task<CartGetDto> GetCartAsync()
    {
        var items = await cartItemRepository.GetAllWithProductAsync();
        return new CartGetDto { Items = items.Select(MapToDto).ToList() };
    }

    public async Task<CartItemGetDto> AddItemAsync(AddCartItemDto dto)
    {
        await productRepository.GetByIdAsync(dto.ProductId); // throws if not found

        var existing = await cartItemRepository.GetByProductIdAsync(dto.ProductId);
        if (existing != null)
        {
            existing.Quantity += dto.Quantity;
            await cartItemRepository.UpdateAsync(existing);
            return MapToDto(existing);
        }

        var item = new Cart { ProductId = dto.ProductId, Quantity = dto.Quantity };
        await cartItemRepository.AddAsync(item);
        var added = await cartItemRepository.GetByIdWithProductAsync(item.Id);
        return MapToDto(added);
    }

    public async Task<CartItemGetDto> UpdateItemAsync(int itemId, UpdateCartItemDto dto)
    {
        var item = await cartItemRepository.GetByIdWithProductAsync(itemId);
        item.Quantity = dto.Quantity;
        await cartItemRepository.UpdateAsync(item);
        return MapToDto(item);
    }

    public Task RemoveItemAsync(int itemId) => cartItemRepository.DeleteAsync(itemId);

    public Task ClearCartAsync() => cartItemRepository.ClearAsync();

    private static CartItemGetDto MapToDto(Cart ci) =>
        new()
        {
            Id = ci.Id,
            ProductId = ci.ProductId,
            ProductName = ci.Product.Name,
            UnitPrice = ci.Product.Price,
            Quantity = ci.Quantity,
        };
}
