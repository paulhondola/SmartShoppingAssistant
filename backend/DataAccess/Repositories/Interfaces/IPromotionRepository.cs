using Backend.DataAccess.Entities;

namespace Backend.DataAccess.Repositories.Interfaces;

public interface IPromotionRepository : IRepository<Promotion>
{
    Task<List<Promotion>> GetForProductAsync(int productId);
}