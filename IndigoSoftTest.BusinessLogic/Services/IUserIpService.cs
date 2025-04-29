using IndigoSoftTest.BusinessLogic.Entities;

namespace IndigoSoftTest.BusinessLogic.Services;

public interface IUserIpService
{
    Task Add(long userName, string ipAddress, IpAddressVersion ipAddressVersion);
}