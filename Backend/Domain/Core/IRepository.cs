namespace Domain.Core;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
{
    public void Insert(TEntity entity);
    public void Update(TEntity entityToUpdate);
    public void Delete(object id);
    public void Delete(TEntity entityToDelete);
    public void Delete(IEnumerable<TEntity> entitiesToDelete);
    public Task InsertAsync(TEntity entity);
    public Task UpdateAsync(TEntity entityToUpdate);
    public Task DeleteAsync(object id);
    public Task DeleteAsync(TEntity entityToDelete);
    public Task DeleteAsync(IEnumerable<TEntity> entityToDelete);
}