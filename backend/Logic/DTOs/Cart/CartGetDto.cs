namespace Logic.DTOs.Cart;

public class CartGetDto
{
    public List<CartItemGetDto> Items { get; set; } = new();
}
