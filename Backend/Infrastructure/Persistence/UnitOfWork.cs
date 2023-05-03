using Domain.Core;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public IRepository<User, Guid> Users { get; }

    private readonly AirleganceDbContext _dbContext;

    public UnitOfWork(AirleganceDbContext dbContext)
    {
        _dbContext = dbContext;
        Users = new Repository<User, Guid>(_dbContext);
    }


    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }
}