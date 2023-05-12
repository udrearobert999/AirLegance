using System.Linq.Expressions;
using Domain.Core;
using Domain.Entities;

namespace UnitTests.Mocks;

internal class MockReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected readonly Dictionary<TKey, TEntity> _store;

    public MockReadOnlyRepository()
    {
        _store = new Dictionary<TKey, TEntity>();
    }

    public MockReadOnlyRepository(IEnumerable<TEntity> entities)
    {
        _store = entities.ToDictionary(e => e.Id, e => e);
    }

    public Task<TEntity?> GetByIdAsync(TKey id, bool track = true)
    {
        _store.TryGetValue(id, out TEntity? entity);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<TEntity>> GetAllAsync(bool track = true)
    {
        return Task.FromResult<IEnumerable<TEntity>>(_store.Values.ToList());
    }

    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool track = true)
    {
        var query = _store.Values.AsQueryable().Where(predicate);
        return Task.FromResult<IEnumerable<TEntity>>(query.ToList());
    }

    public Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, bool track = true)
    {
        var entity = _store.Values.AsQueryable().FirstOrDefault(predicate);
        return Task.FromResult(entity);
    }
}