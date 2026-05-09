using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetByIdsAsync(IEnumerable<int> ids);
}
