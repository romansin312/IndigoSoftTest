using IndigoSoftTest.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoftTest.BusinessLogic.Repositories.Users;

public class UsersRepository(IndigoSoftTestDbContext dbContext) : RepositoryBase<User>(dbContext), IUsersRepository
{
    public Task<User?> GetByUserIdAsync(ulong userId)
    {
        return dbContext.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
    }
}