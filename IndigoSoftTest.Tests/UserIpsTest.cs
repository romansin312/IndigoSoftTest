using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoftTest.Tests;

[TestFixture]
public class Tests
{
    private IUserIpService _userIpService;
    
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase("IndigoSoftTest").Options;
        var dbContext = new IndigoSoftTestDbContext(options);

        _userIpService = new UserIpService(dbContext);
    }
    
    private async Task FillDatabase()
    {
        await _userIpService.AddAsync(100, "127.0.0.1", IpAddressVersion.V4);
        await _userIpService.AddAsync(101, "127.0.0.2", IpAddressVersion.V4);
        await _userIpService.AddAsync(102, "127.0.0.1", IpAddressVersion.V4);
        await _userIpService.AddAsync(103, "127.0.0.3", IpAddressVersion.V4);
        await _userIpService.AddAsync(104, "127.0.0.4", IpAddressVersion.V4);
        await _userIpService.AddAsync(100, "127.0.0.5", IpAddressVersion.V4);
    }

    [Test]
    public async Task TestGetUsersByIp()
    {
        await FillDatabase();
        
        var result = await _userIpService.GetUsersByIp("127.0.0.1");
        var expectedResult = new ulong[] { 100, 102 };

        if (expectedResult.All(r => result.Contains(r)))
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }

    
    [Test]
    public async Task GeUserIpExists()
    {
        await FillDatabase();
        
        var result = await _userIpService.GetUserIp(100,"127.0.0.1");
        
        if (result != null)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public async Task GeUserIpNotExists()
    {
        await FillDatabase();
        
        var result = await _userIpService.GetUserIp(100,"127.0.0.100");
        
        if (result == null)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    
    [Test]
    public async Task GetUserIpsByUserTest()
    {
        await FillDatabase();
        
        var result = await _userIpService.GetIpsByUserId(100);
        var expectedResult = new string[] { "127.0.0.1", "127.0.0.5" };

        if (expectedResult.All(r => result.Contains(r)))
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
}