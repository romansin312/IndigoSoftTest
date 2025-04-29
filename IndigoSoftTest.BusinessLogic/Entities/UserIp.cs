using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoftTest.BusinessLogic.Entities;

public enum IpAddressVersion
{
    V4, V6
}

/// <summary>
/// Table with relations of users and their IP 
/// </summary>
[Table("UserIps")]
[Index("IpAddress")]
public class UserIp
{
    /// <summary>
    /// User identifier
    /// </summary>
    [Key,Required]
    public long UserId { get; set; }
    
    /// <summary>
    /// User's Ip Address
    /// </summary>
    public string? IpAddress { get; set; }
    
    public IpAddressVersion IpAddressVersion { get; set; }
    
    /// <summary>
    /// Last connection date
    /// </summary>
    public DateTime LastConnectionDate { get; set; }
}