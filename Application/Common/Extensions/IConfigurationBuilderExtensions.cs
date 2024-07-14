using Application.Common.Extensions;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Extensions;

/// <summary>
/// Decorator to add file name depending on environment
/// </summary>
public static class IConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder configurationBuilder)
    {
        string fileName;
#if DEBUG
        fileName = "appsettings.Development.json";
#else
    fileName = "appsettings.json";
#endif
        return configurationBuilder.AddJsonFile(fileName, optional: false, reloadOnChange: true);
    }
}
