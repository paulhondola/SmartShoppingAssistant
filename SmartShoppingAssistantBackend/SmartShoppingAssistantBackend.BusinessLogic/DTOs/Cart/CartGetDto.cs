using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Products;

namespace SmartShoppingAssistantBackend.BusinessLogic.DTOs.Cart;

public class CartGetDto
{
    public int Id { get; set; }

    public ProductGetDto Product { get; set; }
    public int Quantity { get; set; }
}