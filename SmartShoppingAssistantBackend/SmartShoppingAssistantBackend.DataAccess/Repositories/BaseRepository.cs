using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistantBackend.DataAccess.Repositories.Interfaces;

namespace SmartShoppingAssistantBackend.DataAccess.Repositories;

public class BaseRepository<TEntity>(SmartShoppingAssistantDbContext context) : IRepository<TEntity>
    where TEntity : class
{
    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity is null) throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found.");

        return entity;
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedEntity = context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }
}