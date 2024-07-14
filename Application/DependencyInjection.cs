using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    /// <summary>
    /// Handles dependency injection in the application layer
    /// </summary>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
    }
}
