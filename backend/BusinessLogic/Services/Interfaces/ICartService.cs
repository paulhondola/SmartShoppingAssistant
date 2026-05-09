using Backend.BusinessLogic.DTOs.Cart;

namespace Backend.BusinessLogic.Services.Interfaces;

public interface ICartService
{
    Task<CartSummaryDto> GetCartAsync();
    Task<CartGetDto> AddItemToCartAsync(CartCreateDto cartCreateDto);
    Task<CartGetDto> UpdateCartItemQuantityAsync(int itemId, CartUpdateDto cartUpdateDto);
    Task DeleteCartItemAsync(int itemId);
    Task DeleteCartAsync();
}