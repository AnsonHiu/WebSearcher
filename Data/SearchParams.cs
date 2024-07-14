namespace Data;

/// <param name="Keywords">Query used for Googling</param>
/// <param name="ReturnCount">Number of results wanted</param>
public record SearchParams
(
    string Keywords,
    int ReturnCount
);
