using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface IPromotionRepository : IRepository<Promotion>
{
    Task<List<Promotion>> GetForProductAsync(int productId);
}
