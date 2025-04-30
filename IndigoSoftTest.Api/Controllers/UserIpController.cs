using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using IndigoSoftTest.Api.Models;
using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Services;

namespace IndigoSoftTest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserIpController(ILogger<UserIpController> logger, IUserIpService userIpService)
    : Controller
{
    private readonly ILogger<UserIpController> _logger = logger;

    [HttpPost("add")]
    public async Task<IActionResult> AddAddress([FromBody] AddUserIpRequest request)
    {
        var ipAddress = ParseIp(request.IpAddress);
        if(ipAddress == null)
        {
            return BadRequest("Invalid IP address.");
        }
        
        await userIpService.AddAsync(request.UserId, ipAddress.ToString(), ipAddress.AddressFamily == AddressFamily.InterNetwork ? IpAddressVersion.V4 :  IpAddressVersion.V6);
        return Created();
    }

    [HttpGet("by-user/all")]
    public async Task<ActionResult<IList<string>>> GetAllIpsByUserId(ulong userId)
    {
        return Ok(await userIpService.GetIpsByUserId(userId));
    }

    [HttpGet("by-user/last-connected")]
    public async Task<ActionResult<LastConnectedIpResponse>> GetLastConnectedIpByUserId(ulong userId)
    {
        var lastConnectedUserIp = await userIpService.GetLastConnectedIp(userId);
        if (lastConnectedUserIp == null)
        {
            return NotFound();
        }

        return Ok(new LastConnectedIpResponse
        {
            Ip = lastConnectedUserIp.IpAddress.Ip,
            LastConnectedDate = lastConnectedUserIp.ConnectionDate
        });
    }

    [HttpGet("by-user/{userId}/ip/{ip}/connection-date")]
    public async Task<ActionResult<DateTime>> GetLastConnectedIpByUserId(ulong userId, string ip)
    {
        var ipParsed = ParseIp(ip);
        if (ipParsed == null)
        {
            return BadRequest("Invalid IP address.");
        }
        
        var userIp = await userIpService.GetUserIp(userId, ip);
        if (userIp == null)
        {
            return NotFound();
        }

        return Ok(userIp.ConnectionDate);
    }

    [HttpGet("users/by-ip")]
    public async Task<ActionResult<IList<ulong>>> GetLastConnectedIpByUserId(string ip)
    {
        return Ok(await userIpService.GetUsersByIp(ip));
    }

    private IPAddress? ParseIp(string ipStr)
    {
        IPAddress.TryParse(ipStr, out var ipAddress);
        return ipAddress;

    }
}