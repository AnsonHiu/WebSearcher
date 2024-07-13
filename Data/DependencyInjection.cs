using Data.Interfaces;
using Data.Options;
using Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyInjection
{
    /// <summary>
    /// Handles dependency injection used in the data layer
    /// </summary>
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISearchService, GoogleSearchService>();
        services.Configure<GoogleCustomSearchApiOptions>(configuration.GetSection(GoogleCustomSearchApiOptions.GoogleCustomSearchApi));
        return services;
    }
}
