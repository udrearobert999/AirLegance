using Domain.Core;
using Domain.Entities;

namespace UnitTests.Mocks;

internal class MockRepository<TEntity, TKey> : MockReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    public MockRepository()
    {
    }

    public MockRepository(IEnumerable<TEntity> entities) : base(entities)
    {
    }

    public void Add(TEntity entity)
    {
        _store[entity.Id] = entity;
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            _store[entity.Id] = entity;
        }
    }

    public void Delete(TEntity entity)
    {
        _store.Remove(entity.Id);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            _store.Remove(entity.Id);
        }
    }

    public void Update(TEntity entity)
    {
        if (_store.ContainsKey(entity.Id))
        {
            _store[entity.Id] = entity;
        }
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (_store.ContainsKey(entity.Id))
            {
                _store[entity.Id] = entity;
            }
        }
    }
}