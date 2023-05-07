namespace Domain.Core;

public interface IUnitOfWork
{
    IUsersRepository Users { get; }

    public Task<int> SaveChangesAsync();

    public int SaveChanges();
}