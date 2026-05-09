using Microsoft.EntityFrameworkCore;
using Backend.DataAccess.Entities;

namespace Backend.DataAccess.Repositories;

public class PromotionRepository(BackendDbContext context)
    : BaseRepository<Promotion>(context)
{
    public async Task<List<Promotion>> GetForProductAsync(int productId)
    {
        var categoryIds = await context.Products
            .Where(p => p.Id == productId)
            .SelectMany(p => p.Categories.Select(c => c.Id))
            .ToListAsync();

        return await GetAllAsQueryable()
            .Where(p => p.IsActive && (p.ProductId == productId ||
                                       (p.CategoryId.HasValue && categoryIds.Contains(p.CategoryId.Value))))
            .ToListAsync();
    }
}