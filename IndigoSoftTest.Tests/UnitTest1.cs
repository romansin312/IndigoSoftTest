using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoftTest.Tests;

[TestFixture]
public class Tests
{
    IUserIpService _userIpService;
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase("IndigoSoftTest").Options;
        var dbContext = new IndigoSoftTestDbContext(options);

        _userIpService = new UserIpService(dbContext);
    }

    [Test]
    public async Task Test1()
    {
        await _userIpService.AddAsync(100, "127.0.0.1", IpAddressVersion.V4);
        
        var result = await _userIpService.GetUsersByIp("127.0.0.1");

        if (result.SingleOrDefault(s => s == 100) != default)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
}