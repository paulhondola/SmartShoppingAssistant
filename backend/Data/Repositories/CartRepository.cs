using Data.Repositories.Interfaces;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CartRepository(SmartShoppingAssistantDbContext context)
    : BaseRepository<Cart>(context),
        ICartRepository
{
    private IQueryable<Cart> WithProduct() => GetAllAsQueryable().Include(ci => ci.Product);

    public async Task<List<Cart>> GetAllWithProductAsync()
    {
        return await WithProduct().ToListAsync();
    }

    public async Task<Cart?> GetByProductIdAsync(int productId)
    {
        return await WithProduct().FirstOrDefaultAsync(ci => ci.ProductId == productId);
    }

    public async Task<Cart> GetByIdWithProductAsync(int id)
    {
        return await WithProduct().FirstOrDefaultAsync(ci => ci.Id == id)
            ?? throw new Exception($"Cart item with id {id} not found");
    }

    public async Task ClearAsync()
    {
        context.Cart.RemoveRange(context.Cart);
        await context.SaveChangesAsync();
    }
}
