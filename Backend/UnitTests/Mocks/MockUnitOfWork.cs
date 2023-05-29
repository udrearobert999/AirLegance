using Domain.Core;
using Domain.Entities;

namespace UnitTests.Mocks;

internal class MockUnitOfWork : IUnitOfWork
{
    private readonly IUsersRepository _usersRepository;

    public MockUnitOfWork(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public IUsersRepository Users => _usersRepository;
    public IReadOnlyRepository<Location, Guid> Location { get; }
    public IFlightsRepository Flights { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await Task.FromResult(0);
    }

    public int SaveChanges()
    {
        return 0;
    }
}