using Data.Seeding.Interfaces;

namespace Data.Seeding;

public class DatabaseSeeder(
    SmartShoppingAssistantDbContext context,
    CategorySeeder categorySeeder,
    ProductSeeder productSeeder,
    PromotionSeeder promotionSeeder,
    CartSeeder cartSeeder
)
{
    public async Task SeedAsync()
    {
        await categorySeeder.SeedAsync();
        await productSeeder.SeedAsync();
        await promotionSeeder.SeedAsync();
        await cartSeeder.SeedAsync();
    }
}
