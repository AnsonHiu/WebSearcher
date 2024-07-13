using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using Domain.Queries.SearchQuery;

namespace Domain;

public static  class DependencyInjection
{
    /// <summary>
    /// Handles dependency injection used in the domain layer
    /// </summary>
    public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.ConfigureLogging(configuration);
        services.ConfigureValidation();
    }

    private static void ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
    {
        var nLogConfiguration = new NLogLoggingConfiguration(configuration.GetSection("Logging").GetSection("NLog"));
        services.AddLogging(builder => builder.AddNLog(nLogConfiguration));
    }

    private static void ConfigureValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<KeywordSearchQuery>, KeywordSearchQueryValidator>();
    }
}
