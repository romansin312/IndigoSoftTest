using IndigoSoftTest.BusinessLogic.Entities;

namespace IndigoSoftTest.BusinessLogic.Services;

public interface IUserIpService
{
    Task AddAsync(ulong userId, string ipAddress, IpAddressVersion ipAddressVersion);
    Task<IList<string>> GetIpsByUserId(ulong userId);
    Task<UserIp?> GetLastConnectedIp(ulong userId);
    Task<UserIp?> GetUserIp(ulong userId, string ip);
    Task<IList<ulong>> GetUsersByIp(string ip);
}