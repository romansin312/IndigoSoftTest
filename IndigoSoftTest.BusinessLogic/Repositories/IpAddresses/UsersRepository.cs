using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoftTest.BusinessLogic.Repositories.IpAddresses;

public class IpAddressesRepository(IndigoSoftTestDbContext dbContext) : RepositoryBase<IpAddress>(dbContext), IIpAddressesRepository
{
    public Task<IpAddress?> GetByIp(string ip)
    {
        return dbContext.IpAddresses.Where(x => x.Ip == ip).FirstOrDefaultAsync();
    }
}