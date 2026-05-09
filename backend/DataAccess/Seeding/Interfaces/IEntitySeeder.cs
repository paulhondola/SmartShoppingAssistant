namespace Backend.DataAccess.Seeding;

public interface IEntitySeeder
{
    Task SeedAsync(BackendDbContext context);
}
