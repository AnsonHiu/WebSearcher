﻿namespace Data.Options;

public class GoogleCustomSearchApiOptions
{
    // TODO: Handle empty options
    public const string GoogleCustomSearchApi = "GoogleCustomSearchApi";
    public string SearchEngineId { get; set; } = String.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
}
