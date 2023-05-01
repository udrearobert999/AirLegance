using Domain.Entities;

namespace Domain.Core;

public interface IUnitOfWork
{
    IRepository<User, Guid> Users { get; }

    public Task<int> SaveChangesAsync();

    public int SaveChanges();
}