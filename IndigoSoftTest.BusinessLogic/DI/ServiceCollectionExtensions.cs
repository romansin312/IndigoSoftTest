using IndigoSoftTest.BusinessLogic.Entities;
using IndigoSoftTest.BusinessLogic.Repositories;
using IndigoSoftTest.BusinessLogic.Repositories.UserIp;
using IndigoSoftTest.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IndigoSoftTest.BusinessLogic.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services) =>
        services.AddDbContext<IndigoSoftTestDbContext>()
                .AddScoped<IUserIpService, UserIpService>()
                .AddScoped<IUserIpRepository, UserIpRepository>();
}