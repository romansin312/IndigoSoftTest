using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Repositories.UserIp;
using IndigoSoftTest.BusinessLogic.Repositories.Users;
using Microsoft.EntityFrameworkCore;

public class UserIpRepository(IndigoSoftTestDbContext dbContext) : RepositoryBase<UserIp>(dbContext), IUserIpRepository
{
    public Task<UserIp?> GetByUserAndIp(ulong userId, Guid ipId)
    {
        return dbContext.UserIps.Where(s => s.UserId == userId && s.IpAddressId == ipId).FirstOrDefaultAsync();
    }
}