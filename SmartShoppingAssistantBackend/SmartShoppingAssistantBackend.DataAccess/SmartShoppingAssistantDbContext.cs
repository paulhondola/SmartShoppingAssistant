using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistantBackend.DataAccess.Configurations;
using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess;

public class SmartShoppingAssistantDbContext(DbContextOptions<SmartShoppingAssistantDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null;
    public DbSet<Category> Categories { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartShoppingAssistantDbContext).Assembly);
    }
}