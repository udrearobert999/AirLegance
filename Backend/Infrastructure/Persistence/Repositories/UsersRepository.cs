using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UsersRepository : Repository<User, Guid>, IUsersRepository
{
    public UsersRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetUserWithRolesAndTokenByEmailAsync(string email)
    {
        var user = await _dbContext.Users.Where(u => u.Email == email)
            .Include(u => u.Roles)
            .Include(u => u.UserToken)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User?> GetUserWithRolesAndTokenByRefreshToken(string token)
    {
        var user = await _dbContext.Users.Where(u => u.UserToken != null && u.UserToken.Token == token)
            .Include(u => u.Roles)
            .Include(u => u.UserToken)
            .FirstOrDefaultAsync();

        return user;
    }
}