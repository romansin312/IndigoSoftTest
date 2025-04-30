using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoftTest.BusinessLogic.Entities;

/// <summary>
/// Users table
/// </summary>
[Table("IpAddresses")]
[Index(nameof(Ip))]
public class IpAddress
{
    /// <summary>
    /// Record identifier
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// IP Address
    /// </summary>
    [Required]
    public string Ip { get; set; }
    
    /// <summary>
    /// IP address version
    /// </summary>
    public IpAddressVersion IpAddressVersion { get; set; }
    
    /// <summary>
    /// Linked user ips
    /// </summary>
    public IEnumerable<UserIp> UserIps { get; set; } = new List<UserIp>();
}