using Domain.Core;

namespace UnitTests.Mocks;

internal class MockUnitOfWork : IUnitOfWork
{
    private readonly IUsersRepository _usersRepository;

    public MockUnitOfWork(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public IUsersRepository Users => _usersRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await Task.FromResult(0);
    }

    public int SaveChanges()
    {
        return 0;
    }
}