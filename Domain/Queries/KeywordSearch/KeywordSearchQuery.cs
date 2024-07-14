using MediatR;

namespace Domain.Queries.SearchQuery;
public record KeywordSearchQuery
(
    string Keywords,
    int MaxCount = 100
) : IRequest<IEnumerable<KeywordSearchQueryResult>>;