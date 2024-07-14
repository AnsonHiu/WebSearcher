using System.ComponentModel.DataAnnotations;

namespace Data.Options;

/// <summary>
/// Google API connection details
/// </summary>
public sealed class GoogleCustomSearchApiOptions
{
    public const string GoogleCustomSearchApi = "GoogleCustomSearchApi";

    [Required]
    public required string SearchEngineId { get; set; }

    [Required]
    public required string ApiKey { get; set; }

    public string ApplicationName { get; set; } = string.Empty;
    public string SearchCountry { get; set; } = string.Empty;
}
