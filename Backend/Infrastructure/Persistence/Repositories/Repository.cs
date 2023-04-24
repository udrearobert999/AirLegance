using System.Linq.Expressions;
using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    public TEntity? GetById(object id, bool track = false)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<TEntity> Get(bool track = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        if (!track)
            return query.AsNoTracking().ToList();

        return query.ToList();
    }

    public TEntity? GetFirstOrDefault(bool track = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        if (!track)
            return query.AsNoTracking().FirstOrDefault();

        return query.FirstOrDefault();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(object id, bool track = false)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(bool track = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        if (!track)
            return await query.AsNoTracking().ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(bool track = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        if (!track)
            return await query.AsNoTracking().FirstOrDefaultAsync();

        return await query.FirstOrDefaultAsync();
    }

    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entityToUpdate)
    {
        _dbSet.Update(entityToUpdate);
    }

    public void Delete(object id)
    {
        var entity = _dbSet.Find(id);
        if (entity is not null)
            _dbSet.Remove(entity);
    }

    public void Delete(TEntity entityToDelete)
    {
        _dbSet.Remove(entityToDelete);
    }

    public void Delete(IEnumerable<TEntity> entitiesToDelete)
    {
        _dbSet.RemoveRange(entitiesToDelete);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity entityToUpdate)
    {
        await Task.Run(() => _dbSet.Update(entityToUpdate));
    }

    public async Task DeleteAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
            await Task.Run(() => _dbSet.Remove(entity));
    }

    public async Task DeleteAsync(TEntity entityToDelete)
    {
        await Task.Run(() => _dbSet.Remove(entityToDelete));
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entityToDelete)
    {
        await Task.Run(() => _dbSet.RemoveRange(entityToDelete));
    }
}