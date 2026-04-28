using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Repositories;

public class ProductRepository(SmartShoppingAssistantDbContext context)
    : BaseRepository<Product>(context)
{
    public override async Task<Product> GetByIdAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.Categories)
            .ThenInclude(c => c.Promotions)
            .Include(p => p.Promotions)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            throw new KeyNotFoundException($"Product with ID {id} not found.");

        return product;
    }

    public override async Task<List<Product>> GetAllAsync()
    {
        return await context.Products
            .Include(p => p.Categories)
            .ThenInclude(c => c.Promotions)
            .Include(p => p.Promotions)
            .ToListAsync();
    }
}