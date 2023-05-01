using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    public Repository(DbContext dbContext) : base(dbContext)
    {
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }
}