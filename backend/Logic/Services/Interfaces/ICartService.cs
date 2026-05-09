using Logic.DTOs.Cart;

namespace Logic.Services.Interfaces;

public interface ICartService
{
    Task<CartGetDto> GetCartAsync();
    Task<CartItemGetDto> AddItemAsync(AddCartItemDto dto);
    Task<CartItemGetDto> UpdateItemAsync(int itemId, UpdateCartItemDto dto);
    Task RemoveItemAsync(int itemId);
    Task ClearCartAsync();
}
