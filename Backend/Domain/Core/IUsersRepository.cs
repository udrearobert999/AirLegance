using Domain.Entities;

namespace Domain.Core;

public interface IUsersRepository : IRepository<User, Guid>
{
    public Task<User?> GetUserWithRolesAndTokenByEmailAsync(string email);
    public Task<User?> GetUserWithRolesAndTokenByRefreshToken(string token);
    public Task<User?> GetUserWithTokenByRefreshToken(string token);
}