using System.Net;
using IndigoSoftTest.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoftTest.BusinessLogic.Services;

public class UserIpService(IndigoSoftTestDbContext dbContext)
    : IUserIpService
{
    public async Task AddAsync(ulong userId, string ipAddress, IpAddressVersion ipAddressVersion)
    {
        var ipAddressEntity = await dbContext.IpAddresses.Where(x => x.Ip == ipAddress).FirstOrDefaultAsync() ?? new IpAddress()
        {
            Ip = ipAddress,
            IpAddressVersion = ipAddressVersion,
            Id = Guid.NewGuid()
        };

        var userEntity = await dbContext.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync() ?? new User()
        {
            UserId = userId
        };
        
        var existingUserIp = await dbContext.UserIps.Where(s => s.UserId == userId && s.IpAddressId == ipAddressEntity.Id).FirstOrDefaultAsync();
        if (existingUserIp != null)
        {
            existingUserIp.ConnectionDate = DateTime.UtcNow;
        }
        else
        {
            await dbContext.AddAsync(new UserIp
            {
                Id = Guid.NewGuid(),
                ConnectionDate = DateTime.UtcNow,
                IpAddress = ipAddressEntity,
                User = userEntity
            });
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<IList<string>> GetIpsByUserId(ulong userId)
    {
        var allUserIps = await dbContext
            .UserIps
            .Where(s => s.UserId == userId)
            .Include(s => s.IpAddress)
            .ToListAsync();
        
        return allUserIps.Select(s => s.IpAddress.Ip).ToList();
    }

    public async Task<UserIp?> GetLastConnectedIp(ulong userId)
    {
        return await dbContext
            .UserIps
            .Include(s => s.IpAddress)
            .OrderByDescending(s => s.ConnectionDate)
            .FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task<UserIp?> GetUserIp(ulong userId, string ip)
    {
        return await dbContext
            .UserIps
            .Include(s => s.IpAddress)
            .FirstOrDefaultAsync(s => s.UserId == userId && s.IpAddress.Ip == ip);
    }

    public async Task<IList<ulong>> GetUsersByIp(string ip)
    {
        var query = dbContext
            .UserIps
            .Include(s => s.IpAddress)
            .AsQueryable();
        
        query = IPAddress.TryParse(ip, out _) ? query.Where(s => s.IpAddress.Ip.StartsWith(ip)) : query.Where(s => s.IpAddress.Ip == ip);
        
        return await query.Select(s => s.UserId).ToListAsync();
    }
}