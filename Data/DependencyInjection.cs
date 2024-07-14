using Data.Interfaces;
using Data.Models.SearchParams;
using Data.Options;
using Data.Services.Google;
using FluentValidation;
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
        services.AddScoped<IGoogleApiFactory, GoogleApiFactory>();
        services.AddScoped<ISearchService, GoogleSearchService>();
        services.AddScoped<IGoogleApiService, GoogleApiService>();
        services.Configure<GoogleCustomSearchApiOptions>(configuration.GetSection(GoogleCustomSearchApiOptions.GoogleCustomSearchApi));
        services.ConfigureValidation();
        return services;
    }

    private static void ConfigureValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<SearchParams>, SearchParamsValidator>();
    }
}
