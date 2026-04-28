using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Seeding;

public class ProductSeeder : IEntitySeeder
{
    public async Task SeedAsync(SmartShoppingAssistantDbContext context)
    {
        if (await context.Products.AnyAsync(p => p.Categories.Any()))
            return;

        // Remove stale products seeded without category associations
        if (await context.Products.AnyAsync())
            context.Products.RemoveRange(await context.Products.ToListAsync());

        // Fetch tracked categories so EF Core writes only join rows, not new category rows.
        var cats = await context.Categories.ToDictionaryAsync(c => c.Name);

        var products = new List<Product>
        {
            // ── Processors ──────────────────────────────────────────────────
            new()
            {
                Name        = "AMD Ryzen 9 7950X",
                Description = "16-core, 32-thread Zen 4 desktop processor with 5.7 GHz boost clock and 170W TDP.",
                Price       = 699.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Ryzen+9+7950X",
                Categories  = [cats["Processors"]],
            },
            new()
            {
                Name        = "Intel Core i9-14900K",
                Description = "24-core (8P+16E) Raptor Lake Refresh processor with 6.0 GHz max turbo and 125W base TDP.",
                Price       = 589.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Core+i9-14900K",
                Categories  = [cats["Processors"]],
            },
            new()
            {
                Name        = "AMD Ryzen 5 7600X",
                Description = "6-core, 12-thread Zen 4 processor at 4.7 GHz base, ideal for mid-range gaming builds.",
                Price       = 249.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Ryzen+5+7600X",
                Categories  = [cats["Processors"]],
            },

            // ── Motherboards ─────────────────────────────────────────────────
            new()
            {
                Name        = "ASUS ROG Strix X670E-E Gaming WiFi",
                Description = "ATX AM5 motherboard with PCIe 5.0, USB4, 2.5G LAN, Wi-Fi 6E, and extensive overclocking controls.",
                Price       = 499.99m,
                ImageUrl    = "https://placehold.co/400x400?text=ROG+X670E-E",
                Categories  = [cats["Motherboards"], cats["Gaming"]],
            },
            new()
            {
                Name        = "MSI MAG B650 TOMAHAWK WIFI",
                Description = "ATX AM5 motherboard with PCIe 5.0 M.2, Wi-Fi 6E, and a 14+2+1 power design for Ryzen 7000.",
                Price       = 239.99m,
                ImageUrl    = "https://placehold.co/400x400?text=B650+Tomahawk",
                Categories  = [cats["Motherboards"]],
            },

            // ── RAM Memory ───────────────────────────────────────────────────
            new()
            {
                Name        = "Corsair Vengeance DDR5 32GB (2x16GB) 6000MHz",
                Description = "Intel XMP 3.0 / AMD EXPO DDR5 kit at CL36 with Corsair's iCUE RGB lighting.",
                Price       = 149.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Vengeance+DDR5",
                Categories  = [cats["RAM Memory"]],
            },
            new()
            {
                Name        = "Kingston Fury Beast DDR5 16GB (2x8GB) 5200MHz",
                Description = "Low-profile DDR5 kit with Intel XMP 3.0 support and plug-and-play compatibility.",
                Price       = 79.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Fury+Beast+DDR5",
                Categories  = [cats["RAM Memory"]],
            },

            // ── Storage ──────────────────────────────────────────────────────
            new()
            {
                Name        = "Samsung 990 Pro 2TB NVMe SSD",
                Description = "PCIe 4.0 NVMe M.2 SSD with 7450/6900 MB/s sequential read/write and thermal throttle guard.",
                Price       = 199.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Samsung+990+Pro",
                Categories  = [cats["Storage"]],
            },
            new()
            {
                Name        = "WD Black SN850X 1TB NVMe SSD",
                Description = "PCIe 4.0 NVMe M.2 SSD optimised for gaming with 7300/6300 MB/s and Xbox compatibility.",
                Price       = 119.99m,
                ImageUrl    = "https://placehold.co/400x400?text=WD+SN850X",
                Categories  = [cats["Storage"], cats["Gaming"]],
            },
            new()
            {
                Name        = "Seagate BarraCuda 4TB HDD",
                Description = "3.5\" SATA 6Gb/s hard drive at 5400 RPM with 256MB cache for bulk storage.",
                Price       = 89.99m,
                ImageUrl    = "https://placehold.co/400x400?text=BarraCuda+4TB",
                Categories  = [cats["Storage"]],
            },

            // ── Graphics Cards ───────────────────────────────────────────────
            new()
            {
                Name        = "NVIDIA GeForce RTX 4090 24GB",
                Description = "Ada Lovelace flagship GPU with 16384 CUDA cores, DLSS 3, and 4K/8K gaming capability.",
                Price       = 1699.99m,
                ImageUrl    = "https://placehold.co/400x400?text=RTX+4090",
                Categories  = [cats["Graphics Cards"], cats["Gaming"]],
            },
            new()
            {
                Name        = "AMD Radeon RX 7900 XTX 24GB",
                Description = "RDNA 3 flagship with 24GB GDDR6, DisplayPort 2.1, and AV1 hardware encode/decode.",
                Price       = 999.99m,
                ImageUrl    = "https://placehold.co/400x400?text=RX+7900+XTX",
                Categories  = [cats["Graphics Cards"], cats["Gaming"]],
            },
            new()
            {
                Name        = "NVIDIA GeForce RTX 4070 Ti Super 16GB",
                Description = "Ada Lovelace mid-high GPU with 8448 CUDA cores, DLSS 3.5, and excellent 1440p performance.",
                Price       = 799.99m,
                ImageUrl    = "https://placehold.co/400x400?text=RTX+4070+Ti+Super",
                Categories  = [cats["Graphics Cards"], cats["Gaming"]],
            },

            // ── Power Supplies ───────────────────────────────────────────────
            new()
            {
                Name        = "Corsair RM1000x 1000W 80+ Gold",
                Description = "Fully modular ATX power supply with Zero RPM fan mode and 10-year warranty.",
                Price       = 199.99m,
                ImageUrl    = "https://placehold.co/400x400?text=RM1000x",
                Categories  = [cats["Power Supplies"]],
            },
            new()
            {
                Name        = "be quiet! Dark Power 13 850W 80+ Titanium",
                Description = "Fully modular PSU with 80+ Titanium efficiency, silent 135mm fan, and ATX 3.0 support.",
                Price       = 249.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Dark+Power+13",
                Categories  = [cats["Power Supplies"]],
            },

            // ── PC Cases ─────────────────────────────────────────────────────
            new()
            {
                Name        = "Fractal Design Torrent ATX",
                Description = "Open-grille mid-tower case with two 180mm front fans and a 140mm rear fan included.",
                Price       = 149.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Torrent+ATX",
                Categories  = [cats["PC Cases"]],
            },
            new()
            {
                Name        = "Lian Li O11 Dynamic EVO",
                Description = "Dual-chamber mid-tower with tempered glass side and top panels, supporting up to 420mm radiators.",
                Price       = 159.99m,
                ImageUrl    = "https://placehold.co/400x400?text=O11+Dynamic+EVO",
                Categories  = [cats["PC Cases"], cats["Gaming"]],
            },

            // ── Cooling ──────────────────────────────────────────────────────
            new()
            {
                Name        = "Noctua NH-D15 Air Cooler",
                Description = "Dual-tower heatsink with two NF-A15 140mm fans, compatible with Intel LGA1700 and AM5.",
                Price       = 99.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Noctua+NH-D15",
                Categories  = [cats["Cooling"]],
            },
            new()
            {
                Name        = "NZXT Kraken Z73 RGB 360mm AIO",
                Description = "360mm all-in-one liquid cooler with 3x120mm fans, 2.36\" LCD display, and NZXT CAM software.",
                Price       = 249.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Kraken+Z73",
                Categories  = [cats["Cooling"], cats["Gaming"]],
            },

            // ── Monitors ─────────────────────────────────────────────────────
            new()
            {
                Name        = "LG 27GN950-B UltraGear 4K 144Hz",
                Description = "27\" Nano IPS 4K (3840×2160) gaming monitor with 1ms GtG, VESA DisplayHDR 600, and G-Sync Compatible.",
                Price       = 699.99m,
                ImageUrl    = "https://placehold.co/400x400?text=LG+27GN950",
                Categories  = [cats["Monitors"], cats["Gaming"]],
            },
            new()
            {
                Name        = "Samsung Odyssey G9 49\" DQHD 240Hz",
                Description = "49\" 1000R curved VA panel at 5120×1440 DQHD, 240Hz, 1ms, with QLED quantum dot backlighting.",
                Price       = 999.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Odyssey+G9",
                Categories  = [cats["Monitors"], cats["Gaming"]],
            },
            new()
            {
                Name        = "Dell UltraSharp U2723QE 4K USB-C",
                Description = "27\" IPS Black 4K monitor with USB-C 90W PD, built-in KVM, and factory-calibrated DeltaE < 2.",
                Price       = 649.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Dell+U2723QE",
                Categories  = [cats["Monitors"]],
            },

            // ── Keyboards ────────────────────────────────────────────────────
            new()
            {
                Name        = "Corsair K100 RGB Mechanical",
                Description = "Full-size mechanical keyboard with Cherry MX Speed switches, per-key RGB, and iCUE OPX optical switches option.",
                Price       = 229.99m,
                ImageUrl    = "https://placehold.co/400x400?text=K100+RGB",
                Categories  = [cats["Keyboards"], cats["Gaming"]],
            },
            new()
            {
                Name        = "Razer BlackWidow V4 Pro Wireless",
                Description = "Full-size 75g wireless mechanical keyboard with Razer Yellow switches, Chroma RGB, and 200hr battery.",
                Price       = 229.99m,
                ImageUrl    = "https://placehold.co/400x400?text=BlackWidow+V4+Pro",
                Categories  = [cats["Keyboards"], cats["Gaming"]],
            },
            new()
            {
                Name        = "Keychron K2 Pro QMK/VIA",
                Description = "75% hot-swap wireless mechanical keyboard with QMK/VIA support and Gateron G Pro switches.",
                Price       = 99.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Keychron+K2+Pro",
                Categories  = [cats["Keyboards"]],
            },

            // ── Mice ─────────────────────────────────────────────────────────
            new()
            {
                Name        = "Logitech G502 X Plus Wireless",
                Description = "Wireless gaming mouse with LIGHTFORCE hybrid switches, HERO 25K sensor, and 89g weight.",
                Price       = 159.99m,
                ImageUrl    = "https://placehold.co/400x400?text=G502+X+Plus",
                Categories  = [cats["Mice"], cats["Gaming"]],
            },
            new()
            {
                Name        = "Razer DeathAdder V3 Pro",
                Description = "Lightweight ergonomic wireless mouse at 63g with Focus Pro 30K optical sensor and 90hr battery.",
                Price       = 149.99m,
                ImageUrl    = "https://placehold.co/400x400?text=DeathAdder+V3+Pro",
                Categories  = [cats["Mice"], cats["Gaming"]],
            },

            // ── Headsets ─────────────────────────────────────────────────────
            new()
            {
                Name        = "SteelSeries Arctis Nova Pro Wireless",
                Description = "Multi-system wireless headset with active noise cancellation, hot-swap battery, and Hi-Res audio.",
                Price       = 349.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Arctis+Nova+Pro",
                Categories  = [cats["Headsets"], cats["Gaming"]],
            },
            new()
            {
                Name        = "HyperX Cloud Alpha Wireless",
                Description = "Wireless gaming headset with 300hr battery life, dual-chamber drivers, and DTS Headphone:X Spatial Audio.",
                Price       = 199.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Cloud+Alpha+Wireless",
                Categories  = [cats["Headsets"], cats["Gaming"]],
            },

            // ── Laptops ──────────────────────────────────────────────────────
            new()
            {
                Name        = "ASUS ROG Zephyrus G14 (2024) AMD",
                Description = "14\" OLED 2K 165Hz laptop with Ryzen 9 8945HS, RTX 4060, 32GB LPDDR5X, 1TB NVMe.",
                Price       = 1799.99m,
                ImageUrl    = "https://placehold.co/400x400?text=ROG+Zephyrus+G14",
                Categories  = [cats["Laptops"], cats["Gaming"]],
            },
            new()
            {
                Name        = "Lenovo ThinkPad X1 Carbon Gen 11",
                Description = "14\" 2.8K IPS business ultrabook with Intel Core i7-1365U, 32GB LPDDR5, 1TB SSD, and 57Wh battery.",
                Price       = 1599.99m,
                ImageUrl    = "https://placehold.co/400x400?text=ThinkPad+X1+Carbon",
                Categories  = [cats["Laptops"]],
            },

            // ── Networking ───────────────────────────────────────────────────
            new()
            {
                Name        = "TP-Link Archer AXE300 Wi-Fi 6E Tri-Band",
                Description = "Tri-band Wi-Fi 6E router with 6GHz band support, 2.5G WAN port, and OneMesh compatibility.",
                Price       = 299.99m,
                ImageUrl    = "https://placehold.co/400x400?text=Archer+AXE300",
                Categories  = [cats["Networking"]],
            },
            new()
            {
                Name        = "ASUS ROG Rapture GT-BE98 Wi-Fi 7",
                Description = "Quad-band Wi-Fi 7 gaming router with 10G WAN, VPN Fusion, and ASUS RangeBoost Plus.",
                Price       = 699.99m,
                ImageUrl    = "https://placehold.co/400x400?text=ROG+GT-BE98",
                Categories  = [cats["Networking"], cats["Gaming"]],
            },
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
