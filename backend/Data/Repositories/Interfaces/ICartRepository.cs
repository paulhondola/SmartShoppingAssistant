using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<List<Cart>> GetAllWithProductAsync();
    Task<Cart?> GetByProductIdAsync(int productId);
    Task<Cart> GetByIdWithProductAsync(int id);
    Task ClearAsync();
}
