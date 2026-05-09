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
        };

        await context.Promotions.AddRangeAsync(promotions);
        await context.SaveChangesAsync();
    }
}
