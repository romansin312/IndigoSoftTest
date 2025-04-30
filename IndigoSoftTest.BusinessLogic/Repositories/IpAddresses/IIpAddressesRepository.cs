using IndigoSoftTest.BusinessLogic.Entities;

namespace IndigoSoftTest.BusinessLogic.Repositories.IpAddresses;

public interface IIpAddressesRepository : IRepository<IpAddress>
{
    Task<IpAddress?> GetByIp(string ip);
}