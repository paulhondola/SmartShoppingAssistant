using Data.Entities;
using Data.Seeding.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeding;

public class CartSeeder : IEntitySeeder
{
    public async Task SeedAsync(SmartShoppingAssistantDbContext context)
    {
        if (context.Cart.Any())
            return;

        // Each product maps to at most one cart row (one-to-one relationship).
        var prods = await context.Products.ToDictionaryAsync(p => p.Name);

        // Scenario: a user building a high-end gaming PC + peripherals.
        var cartItems = new List<Cart>
        {
            new() { ProductId = prods["AMD Ryzen 9 7950X"].Id, Quantity = 1 },
            new() { ProductId = prods["ASUS ROG Strix X670E-E Gaming WiFi"].Id, Quantity = 1 },
            new()
            {
                ProductId = prods["Corsair Vengeance DDR5 32GB (2x16GB) 6000MHz"].Id,
                Quantity = 2,
            },
            new() { ProductId = prods["Samsung 990 Pro 2TB NVMe SSD"].Id, Quantity = 1 },
            new() { ProductId = prods["NVIDIA GeForce RTX 4090 24GB"].Id, Quantity = 1 },
            new() { ProductId = prods["Corsair RM1000x 1000W 80+ Gold"].Id, Quantity = 1 },
            new() { ProductId = prods["Lian Li O11 Dynamic EVO"].Id, Quantity = 1 },
            new() { ProductId = prods["NZXT Kraken Z73 RGB 360mm AIO"].Id, Quantity = 1 },
            new() { ProductId = prods["LG 27GN950-B UltraGear 4K 144Hz"].Id, Quantity = 1 },
            new() { ProductId = prods["Corsair K100 RGB Mechanical"].Id, Quantity = 1 },
            new() { ProductId = prods["Razer DeathAdder V3 Pro"].Id, Quantity = 1 },
            new() { ProductId = prods["SteelSeries Arctis Nova Pro Wireless"].Id, Quantity = 1 },
            new() { ProductId = prods["Razer Kiyo Pro Streaming Camera"].Id, Quantity = 1 },
            new() { ProductId = prods["HyperX QuadCast S USB Microphone"].Id, Quantity = 1 },
            new() { ProductId = prods["Meta Quest 3 256GB VR Headset"].Id, Quantity = 1 },
            new() { ProductId = prods["Elgato Cam Link 4K Capture Device"].Id, Quantity = 1 },
        };

        await context.Cart.AddRangeAsync(cartItems);
        await context.SaveChangesAsync();
    }
}
