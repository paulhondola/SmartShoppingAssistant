using Data.Entities;
using Data.Seeding.Interfaces;

namespace Data.Seeding;

public class CategorySeeder(SmartShoppingAssistantDbContext context) : IEntitySeeder
{
    public async Task SeedAsync()
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
            new()
            {
                Name = "Webcams",
                Description = "High-definition webcams for streaming, video conferencing, and content creation with advanced autofocus and low-light performance.",
            },
            new()
            {
                Name = "Microphones",
                Description = "Professional and consumer-grade microphones including USB, XLR, and wireless options for streaming, podcasting, and recording.",
            },
            new()
            {
                Name = "Speakers",
                Description = "Computer speakers ranging from compact desktop models to high-fidelity stereo systems with Bluetooth and surround sound capabilities.",
            },
            new()
            {
                Name = "UPS / Battery Backup",
                Description = "Uninterruptible power supplies and battery backup systems to protect equipment and ensure continuous operation during power outages.",
            },
            new()
            {
                Name = "VR Headsets",
                Description = "Virtual reality headsets for immersive gaming and entertainment compatible with PC platforms.",
            },
            new()
            {
                Name = "Controllers",
                Description = "Wireless and wired controllers including Xbox, PlayStation, and specialty gaming controllers for PC gaming.",
            },
            new()
            {
                Name = "Streaming Equipment",
                Description = "Professional-grade equipment for content creators including capture cards, stream decks, and lighting solutions.",
            },
            new()
            {
                Name = "Tablets",
                Description = "Android and iOS tablets for productivity, creative work, and entertainment across all price tiers.",
            },
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}
