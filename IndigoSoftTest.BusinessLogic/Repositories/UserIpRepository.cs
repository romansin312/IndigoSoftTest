using IndigoSoftTest.BusinessLogic.Entities;

namespace IndigoSoftTest.BusinessLogic.Repositories;

public class UserIpRepository : IUserIpRepository
{
    private readonly IndigoSoftTestDbContext dbContext;
    public async Task Create(UserIp item)
    {
        await dbContext.UserIps.AddAsync(item);
    }
}