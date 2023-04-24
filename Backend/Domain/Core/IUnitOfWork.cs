namespace Domain.Core;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync();
    public Task DisposeAsync();

    public int SaveChanges();
    public void Dispose();
}