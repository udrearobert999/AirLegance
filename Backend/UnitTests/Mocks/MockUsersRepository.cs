using Domain.Core;
using Domain.Entities;

namespace UnitTests.Mocks;

internal class MockUsersRepository : MockRepository<User, Guid>, IUsersRepository
{
    public MockUsersRepository()
    {
    }

    public MockUsersRepository(IEnumerable<User> users) : base(users)
    {
    }

    public async Task<User?> GetUserWithRolesAndTokenByEmailAsync(string email)
    {
        var user = _store.Values
            .Where(u => u.Email == email)
            .Select(u => new User
            {
                Id = u.Id,
                Email = u.Email,
                Password = u.Password,
                Roles = u.Roles // Assuming Roles is a collection property of the User class
            })
            .FirstOrDefault();

        return await Task.FromResult(user);
    }

    public Task<User?> GetUserWithRolesAndTokenByRefreshToken(string token)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserWithTokenByRefreshToken(string token)
    {
        throw new NotImplementedException();
    }
}