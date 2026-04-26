using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Seeding;

public static class DataSeeder
{
    public static async Task SeedAsync(SmartShoppingAssistantDbContext context)
    {
        if (context.Products.Any())
            return;

        var products = new List<Product>
        {
            new()
            {
                Name = "Wireless Noise-Cancelling Headphones",
                Description =
                    "Over-ear headphones with active noise cancellation and 30-hour battery life.",
                Price = 149.99m,
                ImageUrl = "https://placehold.co/400x400?text=Headphones",
            },
            new()
            {
                Name = "Mechanical Keyboard",
                Description =
                    "Compact TKL mechanical keyboard with Cherry MX switches and RGB backlight.",
                Price = 89.99m,
                ImageUrl = "https://placehold.co/400x400?text=Keyboard",
            },
            new()
            {
                Name = "USB-C Hub 7-in-1",
                Description =
                    "Multiport adapter with HDMI 4K, 3x USB-A, SD card reader, and 100W PD.",
                Price = 45.00m,
                ImageUrl = "https://placehold.co/400x400?text=USB+Hub",
            },
            new()
            {
                Name = "Ergonomic Office Chair",
                Description = "Adjustable lumbar support, breathable mesh back, and 4D armrests.",
                Price = 319.00m,
                ImageUrl = "https://placehold.co/400x400?text=Chair",
            },
            new()
            {
                Name = "27\" 4K Monitor",
                Description = "IPS panel, 144Hz refresh rate, HDR400, and USB-C connectivity.",
                Price = 499.99m,
                ImageUrl = "https://placehold.co/400x400?text=Monitor",
            },
            new()
            {
                Name = "Portable SSD 1TB",
                Description = "USB 3.2 Gen 2 external SSD with up to 1050MB/s read speed.",
                Price = 99.99m,
                ImageUrl = "https://placehold.co/400x400?text=SSD",
            },
            new()
            {
                Name = "Webcam 1080p",
                Description =
                    "Full HD webcam with built-in stereo microphone and auto light correction.",
                Price = 69.99m,
                ImageUrl = "https://placehold.co/400x400?text=Webcam",
            },
            new()
            {
                Name = "Smart LED Desk Lamp",
                Description =
                    "Touch-dimming lamp with USB-A charging port and adjustable colour temperature.",
                Price = 34.99m,
                ImageUrl = "https://placehold.co/400x400?text=Lamp",
            },
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
