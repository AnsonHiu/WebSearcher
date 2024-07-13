using MediatR;

namespace Domain.Queries.SearchQuery;
public record KeywordSearchQuery
(
    string Keyword,
    int MaxCount = 100
) : IRequest<IEnumerable<KeywordSearchQueryResult>>;