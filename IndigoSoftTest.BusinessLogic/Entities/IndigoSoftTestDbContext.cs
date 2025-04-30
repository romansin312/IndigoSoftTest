using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IndigoSoftTest.BusinessLogic.Entities;

public sealed class IndigoSoftTestDbContext() : DbContext()
{
    public DbSet<UserIp> UserIps { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<IpAddress> IpAddresses { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder?.UseNpgsql("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=indigosofttest;");
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