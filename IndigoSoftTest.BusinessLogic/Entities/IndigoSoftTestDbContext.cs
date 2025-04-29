using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IndigoSoftTest.BusinessLogic.Entities;

internal sealed class IndigoSoftTestDbContext() : DbContext()
{
    public DbSet<UserIp> UserIps { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder?.UseNpgsql("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=indigosofttest;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;");
    }
}