using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace IndigoSoftTest.BusinessLogic.Entities;

public sealed class IndigoSoftTestDbContext(IConfiguration configuration) : DbContext()
{
    public DbSet<UserIp> UserIps { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<IpAddress> IpAddresses { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder?.UseNpgsql(configuration.GetConnectionString("Postgres"), x => x.MigrationsAssembly("IndigoSoftTest.BusinessLogic"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserIp>()
            .HasOne<User>(s => s.User)
            .WithMany(s => s.UserIps)
            .HasForeignKey(s => s.UserId).IsRequired();

        modelBuilder.Entity<UserIp>()
            .HasOne<IpAddress>(s => s.IpAddress)
            .WithMany(s => s.UserIps)
            .HasForeignKey(s => s.IpAddressId).IsRequired();
    }
}