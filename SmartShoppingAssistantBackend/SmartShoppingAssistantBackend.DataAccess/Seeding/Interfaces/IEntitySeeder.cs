namespace SmartShoppingAssistantBackend.DataAccess.Seeding;

public interface IEntitySeeder
{
    Task SeedAsync(SmartShoppingAssistantDbContext context);
}
