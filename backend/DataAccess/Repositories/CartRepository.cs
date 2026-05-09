using Microsoft.EntityFrameworkCore;
using Backend.DataAccess.Entities;

namespace Backend.DataAccess.Repositories;

public class CartRepository(BackendDbContext context)
    : BaseRepository<Cart>(context)
{
    public override async Task<Cart> GetByIdAsync(int id)
    {
        var cartItem = await context.Cart
            .Include(c => c.Product)
                .ThenInclude(p => p.Promotions)
            .Include(c => c.Product)
                .ThenInclude(p => p.Categories)
                    .ThenInclude(cat => cat.Promotions)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cartItem is null)
            throw new KeyNotFoundException($"Cart item with ID {id} not found.");

        return cartItem;
    }

    public override async Task<List<Cart>> GetAllAsync()
    {
        return await context.Cart
            .Include(c => c.Product)
                .ThenInclude(p => p.Promotions)
            .Include(c => c.Product)
                .ThenInclude(p => p.Categories)
                    .ThenInclude(cat => cat.Promotions)
            .ToListAsync();
    }
}