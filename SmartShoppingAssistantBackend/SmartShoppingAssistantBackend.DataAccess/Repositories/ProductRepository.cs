using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Repositories;

public class ProductRepository<TEntity>(SmartShoppingAssistantDbContext context)
    : BaseRepository<Product>(context), IRepository<Product>
{
    
}