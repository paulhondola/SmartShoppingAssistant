using Data.Repositories.Interfaces;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CategoryRepository(SmartShoppingAssistantDbContext context)
    : BaseRepository<Category>(context),
        ICategoryRepository
{
    public async Task<List<Category>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await GetAllAsQueryable().Where(c => ids.Contains(c.Id)).ToListAsync();
    }
}
