using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using IndigoSoftTest.Api.Models;
using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Services;

namespace IndigoSoftTest.Api.Controllers;

public class UserIpController : Controller
{
    private readonly ILogger<UserIpController> _logger;
    private readonly IUserIpService _userIpService;

    public UserIpController(ILogger<UserIpController> logger, IUserIpService userIpService)
    {
        _logger = logger;
        _userIpService = userIpService;
    }

    public async Task<IActionResult> AddAddress([FromBody] AddUserIpRequest request)
    {
        var ipAddress = ParseIp(request.IpAddress);
        if(ipAddress == null)
        {
            return BadRequest("Invalid IP address.");
        }
        
        await _userIpService.AddAsync(request.UserId, ipAddress.ToString(), ipAddress.AddressFamily == AddressFamily.InterNetwork ? IpAddressVersion.V4 :  IpAddressVersion.V6);
        return Created();
    }

    private IPAddress? ParseIp(string ipStr)
    {
        IPAddress.TryParse(ipStr, out var ipAddress);
        return ipAddress;

    }
}