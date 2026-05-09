namespace Logic.DTOs.Cart;

public class CartGetDto
{
    public List<CartItemGetDto> Items { get; set; } = [];
    public decimal Subtotal { get; set; }
    public List<AppliedPromotionDto> AppliedPromotions { get; set; } = [];
    public decimal TotalDiscount { get; set; }
    public decimal Total { get; set; }
}
