namespace IndigoSoftTest.Api.Models;

public class AddUserIpRequest
{
    public string IpAddress { get; set; }
    public ulong UserId { get; set; }
}