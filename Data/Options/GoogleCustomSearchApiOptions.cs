namespace Data.Options;

/// <summary>
/// Google API connection details
/// </summary>
public class GoogleCustomSearchApiOptions
{
    // TODO: Handle empty options
    public const string GoogleCustomSearchApi = "GoogleCustomSearchApi";
    public string SearchEngineId { get; set; } = String.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
    public string SearchCountry { get; set; } = string.Empty;
}
