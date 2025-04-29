using System.Net;
using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Repositories;

namespace IndigoSoftTest.BusinessLogic.Services;

public enum IpAdressVersion
{
    V4, V6
}

public class UserIpService : IUserIpService
{
    private readonly IUserIpRepository _userIpRepository;

    public UserIpService(IUserIpRepository userIpRepository)
    {
        _userIpRepository = userIpRepository;
    }
    
    public async Task Add(long userName, string ipAddress, IpAddressVersion ipAddressVersion)
    {
        await _userIpRepository.Create(new UserIp
        {
            UserId = userName,
            IpAddressVersion = ipAddressVersion,
            LastConnectionDate = DateTime.Now,
            IpAddress = ipAddress
        });
    }
}