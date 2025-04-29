using Microsoft.AspNetCore.Mvc;
using IndigoSoftTest.Api.Models;

namespace IndigoSoftTest.Api.Controllers;

public class UserIpController : Controller
{
    private readonly ILogger<UserIpController> _logger;

    public UserIpController(ILogger<UserIpController> logger)
    {
        _logger = logger;
    }

    public IActionResult AddAddress([FromBody] AddUserIpRequest request)
    {
        return Ok();
    }
}