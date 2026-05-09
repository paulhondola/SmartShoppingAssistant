using Data.Entities;
using Data.Seeding.Interfaces;

namespace Data.Seeding;

public class CategorySeeder : IEntitySeeder
{
    public async Task SeedAsync(SmartShoppingAssistantDbContext context)
    {
        if (context.Categories.Any())
            return;

        var categories = new List<Category>
        {
            new()
            {
                Name = "Processors",
                Description = "Desktop and laptop CPUs from Intel and AMD.",
            },
            new()
            {
                Name = "Motherboards",
                Description = "ATX, mATX, and ITX motherboards for all sockets.",
            },
            new()
            {
                Name = "RAM Memory",
                Description = "DDR4 and DDR5 desktop and laptop memory kits.",
            },
            new()
            {
                Name = "Storage",
                Description = "NVMe SSDs, SATA SSDs, and HDDs for every workload.",
            },
            new()
            {
                Name = "Graphics Cards",
                Description = "NVIDIA and AMD discrete GPUs for gaming and content creation.",
            },
            new()
            {
                Name = "Power Supplies",
                Description = "80+ certified ATX power supplies from 550W to 1600W.",
            },
            new()
            {
                Name = "PC Cases",
                Description = "Mid-tower, full-tower, and ITX cases with airflow-first designs.",
            },
            new()
            {
                Name = "Cooling",
                Description = "Air coolers, AIO liquid coolers, and case fans.",
            },
            new()
            {
                Name = "Monitors",
                Description = "1080p, 1440p, and 4K monitors with high refresh rates.",
            },
            new()
            {
                Name = "Keyboards",
                Description = "Mechanical, membrane, and wireless keyboards.",
            },
            new() { Name = "Mice", Description = "Wired and wireless gaming and office mice." },
            new()
            {
                Name = "Headsets",
                Description = "Stereo and surround-sound gaming and office headsets.",
            },
            new()
            {
                Name = "Laptops",
                Description = "Gaming laptops, ultrabooks, and workstation notebooks.",
            },
            new()
            {
                Name = "Networking",
                Description = "Wi-Fi routers, switches, and network cards.",
            },
            new()
            {
                Name = "Gaming",
                Description = "Products optimised for gaming performance and aesthetics.",
            },
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}
