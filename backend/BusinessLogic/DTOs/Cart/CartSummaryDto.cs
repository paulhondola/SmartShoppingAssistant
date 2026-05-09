namespace Backend.BusinessLogic.DTOs.Cart;

public class CartSummaryDto
{
    public List<CartItemDto> Items { get; set; } = [];
    public decimal Subtotal { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal FinalPrice { get; set; }
}
