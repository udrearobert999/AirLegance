using System.Linq.Expressions;
using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected readonly AirleganceDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public ReadOnlyRepository(DbContext dbContext)
    {
        _dbContext = (AirleganceDbContext)dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, bool track = true)
    {
        if (track)
            return await _dbSet.FindAsync(id);

        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(bool track = true)
    {
        if (track)
            return await _dbSet.ToListAsync();

        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool track = true)
    {
        if (track)
            return await _dbSet.Where(predicate).ToListAsync();

        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, bool track = true)
    {
        if (track)
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();

        return await _dbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
    }
}