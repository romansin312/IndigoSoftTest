using System.Net;
using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Repositories;
using IndigoSoftTest.BusinessLogic.Repositories.IpAddresses;
using IndigoSoftTest.BusinessLogic.Repositories.UserIp;
using IndigoSoftTest.BusinessLogic.Repositories.Users;

namespace IndigoSoftTest.BusinessLogic.Services;

public enum IpAdressVersion
{
    V4, V6
}

public class UserIpService(IUserIpRepository userIpRepository, IUsersRepository usersRepository, IIpAddressesRepository ipAddressesRepository)
    : IUserIpService
{
    public async Task AddAsync(ulong userId, string ipAddress, IpAddressVersion ipAddressVersion)
    {
        var ipAddressEntity = await ipAddressesRepository.GetByIp(ipAddress) ?? new IpAddress()
        {
            Ip = ipAddress,
            IpAddressVersion = ipAddressVersion,
            Id = Guid.NewGuid()
        };

        var userEntity = await usersRepository.GetByUserIdAsync(userId) ?? new User()
        {
            UserId = userId
        };
        
        var existingUserIp = await userIpRepository.GetByUserAndIp(userEntity.UserId, ipAddressEntity.Id);
        if (existingUserIp != null)
        {
            existingUserIp.ConnectionDate = DateTime.UtcNow;
        }
        else
        {
            await userIpRepository.CreateAsync(new UserIp
            {
                Id = Guid.NewGuid(),
                ConnectionDate = DateTime.UtcNow,
                IpAddress = ipAddressEntity,
                User = userEntity
            });
        }
    }
}