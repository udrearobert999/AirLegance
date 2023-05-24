using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Core;

public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteBy(Expression<Func<TEntity, bool>> predicate);
    void DeleteRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
}