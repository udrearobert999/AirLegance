using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UsersRepository : Repository<User, Guid>, IUsersRepository
{
    public UsersRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetUserWithRolesByEmailAsync(string email)
    { 
        var user = await _dbContext.Users.Where(u => u.Email == email)
            .Include(u => u.Roles)
            .FirstOrDefaultAsync();

        return user;
    }
}