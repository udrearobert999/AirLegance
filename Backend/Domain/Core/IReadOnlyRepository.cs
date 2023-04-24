using System.Linq.Expressions;

namespace Domain.Core;

public interface IReadOnlyRepository<TEntity> where TEntity : class
{
    public IEnumerable<TEntity> GetAll();

    public TEntity? GetById(object id, bool track = false);

    public IEnumerable<TEntity> Get(bool track = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes);

    public TEntity? GetFirstOrDefault(bool track = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes);

    public Task<IEnumerable<TEntity>> GetAllAsync();

    public Task<TEntity?> GetByIdAsync(object id, bool track = false);

    public Task<IEnumerable<TEntity>> GetAsync(bool track = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes);

    public Task<TEntity?> GetFirstOrDefaultAsync(bool track = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes);
}