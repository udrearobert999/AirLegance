using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Core;

public interface IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    Task<TEntity?> GetByIdAsync(TKey id, bool track = true);
    Task<IEnumerable<TEntity>> GetAllAsync(bool track = true);
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool track = true);
    Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, bool track = true);
}