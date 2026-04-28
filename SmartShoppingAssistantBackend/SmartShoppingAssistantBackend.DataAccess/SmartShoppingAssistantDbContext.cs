using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess;

public class SmartShoppingAssistantDbContext(
    DbContextOptions<SmartShoppingAssistantDbContext> options
) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Promotion> Promotions { get; set; } = null!;
    public DbSet<Cart> Cart { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("smart-shopping-assistant");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SmartShoppingAssistantDbContext).Assembly
        );
    }
}