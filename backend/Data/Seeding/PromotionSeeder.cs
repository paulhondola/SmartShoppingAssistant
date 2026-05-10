using Data.Entities;
using Data.Entities.Enums;
using Data.Seeding.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeding;

public class PromotionSeeder : IEntitySeeder
{
    public async Task SeedAsync(SmartShoppingAssistantDbContext context)
    {
        if (context.Promotions.Any())
            return;

        var cats = await context.Categories.ToDictionaryAsync(c => c.Name);
        var prods = await context.Products.ToDictionaryAsync(p => p.Name);

        var promotions = new List<Promotion>
        {
            // ── Cart-wide ────────────────────────────────────────────────────
            new()
            {
                Name = "Summer Sale: 15% off orders over €1000",
                Type = PromotionType.CartTotal,
                Threshold = 1000m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 15,
                IsActive = true,
            },
            new()
            {
                Name = "Mega Cart: 20% off orders over €2500",
                Type = PromotionType.CartTotal,
                Threshold = 2500m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 20,
                IsActive = true,
            },
            // ── Category: Processors ─────────────────────────────────────────
            new()
            {
                Name = "CPU Deal: 10% off any processor",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 10,
                CategoryId = cats["Processors"].Id,
                IsActive = true,
            },
            // ── Category: Gaming ─────────────────────────────────────────────
            new()
            {
                Name = "Gaming Fest: Buy 3 gaming products, get 1 free",
                Type = PromotionType.Quantity,
                Threshold = 3m,
                Reward = PromotionReward.FreeItems,
                RewardValue = 1,
                CategoryId = cats["Gaming"].Id,
                IsActive = true,
            },
            // ── Category: Monitors ───────────────────────────────────────────
            new()
            {
                Name = "Monitor Monday: 20% off when buying 2+ monitors",
                Type = PromotionType.Quantity,
                Threshold = 2m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 20,
                CategoryId = cats["Monitors"].Id,
                IsActive = true,
            },
            // ── Category: Laptops ────────────────────────────────────────────
            new()
            {
                Name = "Back to School: 25% off laptops over €1500",
                Type = PromotionType.CartTotal,
                Threshold = 1500m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 25,
                CategoryId = cats["Laptops"].Id,
                IsActive = false, // upcoming — not yet active
            },
            // ── Category: Storage ────────────────────────────────────────────
            new()
            {
                Name = "Storage Bundle: Buy 3 storage products, get 1 free",
                Type = PromotionType.Quantity,
                Threshold = 3m,
                Reward = PromotionReward.FreeItems,
                RewardValue = 1,
                CategoryId = cats["Storage"].Id,
                IsActive = true,
            },
            // ── Category: Cooling ────────────────────────────────────────────
            new()
            {
                Name = "Cool Down Sale: 12% off all coolers",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 12,
                CategoryId = cats["Cooling"].Id,
                IsActive = true,
            },
            // ── Product-specific ─────────────────────────────────────────────
            new()
            {
                Name = "RTX 4090 Launch Promo: 5% off",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 5,
                ProductId = prods["NVIDIA GeForce RTX 4090 24GB"].Id,
                IsActive = true,
            },
            new()
            {
                Name = "Samsung Week: 8% off Samsung 990 Pro 2TB",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 8,
                ProductId = prods["Samsung 990 Pro 2TB NVMe SSD"].Id,
                IsActive = true,
            },
            new()
            {
                Name = "ROG Laptop Bundle: Buy Zephyrus G14, get 15% off peripherals (€1500+ cart)",
                Type = PromotionType.CartTotal,
                Threshold = 1500m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 15,
                ProductId = prods["ASUS ROG Zephyrus G14 (2024) AMD"].Id,
                IsActive = true,
            },
            // ── Category: Motherboards ───────────────────────────────────────
            new()
            {
                Name = "Motherboard Month: 15% off any motherboard",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 15,
                CategoryId = cats["Motherboards"].Id,
                IsActive = true,
            },
            // ── Category: RAM Memory ─────────────────────────────────────────
            new()
            {
                Name = "Memory Upgrade: Buy 2 RAM kits, get 12% off",
                Type = PromotionType.Quantity,
                Threshold = 2m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 12,
                CategoryId = cats["RAM Memory"].Id,
                IsActive = true,
            },
            // ── Category: Graphics Cards ─────────────────────────────────────
            new()
            {
                Name = "GPU Powerhouse: 18% off GPU orders over €400",
                Type = PromotionType.CartTotal,
                Threshold = 400m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 18,
                CategoryId = cats["Graphics Cards"].Id,
                IsActive = true,
            },
            // ── Category: Power Supplies ─────────────────────────────────────
            new()
            {
                Name = "PSU Bundle: Buy 2 power supplies, get 1 free",
                Type = PromotionType.Quantity,
                Threshold = 2m,
                Reward = PromotionReward.FreeItems,
                RewardValue = 1,
                CategoryId = cats["Power Supplies"].Id,
                IsActive = true,
            },
            // ── Category: PC Cases ───────────────────────────────────────────
            new()
            {
                Name = "Case Upgrade: 20% off PC cases",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 20,
                CategoryId = cats["PC Cases"].Id,
                IsActive = true,
            },
            // ── Category: Keyboards ──────────────────────────────────────────
            new()
            {
                Name = "Peripherals Pair: Buy 2+ keyboards, get 15% off",
                Type = PromotionType.Quantity,
                Threshold = 2m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 15,
                CategoryId = cats["Keyboards"].Id,
                IsActive = true,
            },
            // ── Product-specific ─────────────────────────────────────────────
            new()
            {
                Name = "Intel i9 Performance: 8% off Intel Core i9-14900K",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 8,
                ProductId = prods["Intel Core i9-14900K"].Id,
                IsActive = true,
            },
            new()
            {
                Name = "Corsair DDR5 Elite: 10% off Corsair Vengeance DDR5 32GB",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 10,
                ProductId = prods["Corsair Vengeance DDR5 32GB (2x16GB) 6000MHz"].Id,
                IsActive = true,
            },
            new()
            {
                Name = "Logitech Precision: 12% off G502 X Plus Wireless",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 12,
                ProductId = prods["Logitech G502 X Plus Wireless"].Id,
                IsActive = true,
            },
            new()
            {
                Name = "Back to Campus: 7% off ASUS ROG Zephyrus G14",
                Type = PromotionType.Quantity,
                Threshold = 1m,
                Reward = PromotionReward.PercentDiscount,
                RewardValue = 7,
                ProductId = prods["ASUS ROG Zephyrus G14 (2024) AMD"].Id,
                IsActive = false, // upcoming — not yet active
            },
        };

        await context.Promotions.AddRangeAsync(promotions);
        await context.SaveChangesAsync();
    }
}
