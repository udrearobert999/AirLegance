using Domain.Entities;

namespace Domain.Core;

public interface IUsersRepository : IRepository<User, Guid>
{
    public Task<User?> GetUserWithRolesByEmailAsync(string email);
}