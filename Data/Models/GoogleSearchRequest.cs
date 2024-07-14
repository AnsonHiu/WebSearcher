namespace Data.Models;

public record GoogleSearchRequest
(
    string Keyword,
    int Skip,
    int FetchCount
);
