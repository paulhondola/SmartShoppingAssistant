using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Cart;
using SmartShoppingAssistantBackend.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistantBackend.DataAccess.Entities;
using SmartShoppingAssistantBackend.DataAccess.Repositories.Interfaces;

namespace SmartShoppingAssistantBackend.BusinessLogic.Services;

public class CartService(IRepository<Cart> cartRepository, IRepository<Product> productRepository) : ICartService
{
    public async Task<List<CartGetDto>> GetCartAsync()
    {
        var items = await cartRepository.GetAllAsync();
        return items.Select(MapToDto).ToList();
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

    private static CartGetDto MapToDto(Cart cart)
    {
        return new CartGetDto
        {
            Id = cart.Id,
            ProductId = cart.ProductId,
            Quantity = cart.Quantity
        };
    }
}