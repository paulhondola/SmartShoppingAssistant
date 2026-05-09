namespace Data.Seeding.Interfaces;

public interface IEntitySeeder
{
    Task SeedAsync(SmartShoppingAssistantDbContext context);
}
