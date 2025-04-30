using IndigoSoftTest.BusinessLogic.Entities;

namespace IndigoSoftTest.BusinessLogic.Services;

public interface IUserIpService
{
    Task AddAsync(ulong userId, string ipAddress, IpAddressVersion ipAddressVersion);
}