using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IndigoSoftTest.BusinessLogic.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<IndigoSoftTestDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres"), x => x.MigrationsAssembly("IndigoSoftTest.BusinessLogic")))
            .AddScoped<IUserIpService, UserIpService>();
    }
}