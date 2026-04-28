using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product> GetAllProductsWithCategoriesAsync();
    Task<Product> GetProductWithCategoryAsync(int id);
}