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
[Index(nameof(IpAddressId))]
[Index(nameof(UserId))]
[PrimaryKey(nameof(Id))]
public class UserIp
{
    /// <summary>
    /// Record identifier
    /// </summary>
    [Key]
    public Guid Id { get; init; }
    
    /// <summary>
    /// Users table ID
    /// </summary>
    [Required]
    public ulong UserId { get; init; }
    
    /// <summary>
    /// Ip Addresses table ID
    /// </summary>
    [Required]
    public Guid IpAddressId { get; init; }

    /// <summary>
    /// Connection date
    /// </summary>
    public DateTime ConnectionDate { get; set; }

    /// <summary>
    /// User navigation property
    /// </summary>
    public required User User { get; init; }
    
    /// <summary>
    /// Ip address navigation property
    /// </summary>
    public required IpAddress IpAddress { get; init; }
}