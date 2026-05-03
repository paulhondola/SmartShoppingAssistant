using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Cart;
using SmartShoppingAssistantBackend.BusinessLogic.Services.Interfaces;

namespace SmartShoppingAssistantBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(ICartService cartService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var cartItems = await cartService.GetCartAsync();
        return Ok(cartItems);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] CartCreateDto dto)
    {
        try
        {
            var cartItem = await cartService.AddItemToCartAsync(dto);
            return Ok(cartItem);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("items/{itemId:int}")]
    public async Task<IActionResult> UpdateItemQuantity(int itemId, [FromBody] CartUpdateDto dto)
    {
        try
        {
            var updatedCartItem = await cartService.UpdateCartItemQuantityAsync(itemId, dto);
            return Ok(updatedCartItem);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("items/{itemId:int}")]
    public async Task<IActionResult> DeleteItem(int itemId)
    {
        try
        {
            await cartService.DeleteCartItemAsync(itemId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> ResetCart()
    {
        await cartService.DeleteCartAsync();
        return NoContent();
    }
}