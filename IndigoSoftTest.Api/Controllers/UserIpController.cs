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

    /// <summary>
    /// Add a new user-ip record
    /// </summary>
    /// <param name="request">Request body containing UserId and Ip Address</param>
    /// <response code="200"></response>
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

    /// <summary>
    /// Find all IP addresses for a specified User ID
    /// </summary>
    /// <param name="userId"></param>
    /// <response code="200">List of IP addresses or an empty list if there is no any record</response>
    [HttpGet("by-user/{userId}/all")]
    public async Task<ActionResult<IList<string>>> GetAllIpsByUserId(ulong userId)
    {
        return Ok(await userIpService.GetIpsByUserId(userId));
    }

    /// <summary>
    /// Get the latest connection for a specified user ID
    /// </summary>
    /// <param name="userId"></param>
    /// <response code="200">IP address and Last connection date</response>
    [HttpGet("by-user/{userId}/last-connected")]
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

    /// <summary>
    /// Get a date and time of the latest connection for specified User ID and Ip Address
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ip"></param>
    /// <response code="200">Date and Time in the ISO 8601 format</response>
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

    /// <summary>
    /// Get users by full or beginning of an IP address
    /// </summary>
    /// <param name="ip"></param>
    /// <response code="200">User IDs list</response>
    [HttpGet("users/by-ip/{ip}")]
    public async Task<ActionResult<IList<ulong>>> GetUsersByIp(string ip)
    {
        return Ok(await userIpService.GetUsersByIp(ip));
    }

    private IPAddress? ParseIp(string ipStr)
    {
        IPAddress.TryParse(ipStr, out var ipAddress);
        return ipAddress;

    }
}