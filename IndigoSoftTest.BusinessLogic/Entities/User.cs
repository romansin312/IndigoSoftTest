using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoftTest.BusinessLogic.Entities;

/// <summary>
/// Users table
/// </summary>
[Table("Users")]
[Index(nameof(UserId))]
public class User
{
    /// <summary>
    /// User identifier
    /// </summary>
    [Required, Key]
    public ulong UserId { get; set; }
    
    public IEnumerable<UserIp> UserIps { get; set; } = new List<UserIp>();
}