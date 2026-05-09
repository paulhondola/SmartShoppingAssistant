namespace Backend.DataAccess.Seeding;

public static class DataSeeder
{
    public static async Task SeedAsync(BackendDbContext context)
    {
        IEntitySeeder[] seeders =
        [
            new CategorySeeder(),
            new ProductSeeder(),
            new PromotionSeeder(),
            new CartSeeder(),
        ];

        foreach (var seeder in seeders)
            await seeder.SeedAsync(context);
    }
}
