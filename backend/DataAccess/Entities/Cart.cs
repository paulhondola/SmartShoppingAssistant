namespace Backend.DataAccess.Entities;

public class Cart
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    // Navigation properties
    public Product Product { get; set; } = null!;
}
