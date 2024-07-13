using Data;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;


namespace Application;

public partial class App : System.Windows.Application
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public App()
    {
        //TODO: Error Handling for missing parent directory
        string? projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
        var builder = new ConfigurationBuilder()
            .SetBasePath(projectDirectory ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _configuration = builder.Build();
        var serviceCollection = new ServiceCollection();
        AddApplicationServices(serviceCollection);
        serviceCollection.AddDataServices(_configuration);
        serviceCollection.AddDomainServices(_configuration);

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    /// <summary>
    /// Handles dependency injection in the application layer
    /// </summary>
    private void AddApplicationServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
    }

    /// <summary>
    /// Main entry for Application, runs after App is initialized
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}

