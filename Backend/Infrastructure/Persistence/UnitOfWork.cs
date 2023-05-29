using Domain.Core;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public IUsersRepository Users { get; }
    public IReadOnlyRepository<Location, Guid> Location { get; }
    public IFlightsRepository Flights { get; }

    private readonly AirleganceDbContext _dbContext;

    public UnitOfWork(AirleganceDbContext dbContext)
    {
        _dbContext = dbContext;
        Users = new UsersRepository(_dbContext);
        Location = new ReadOnlyRepository<Location, Guid>(_dbContext);
        Flights = new FlightsRepository(_dbContext);
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