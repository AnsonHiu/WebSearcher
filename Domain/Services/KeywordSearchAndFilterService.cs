using Domain.Entities;
using Domain.Interfaces;
using Domain.Queries.SearchQuery;
using MediatR;

namespace Domain.Services;

public class KeywordSearchAndFilterService(IMediator mediator) : ISearchAndFilterService
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Search via keyword, filter search results with url
    /// </summary>
    /// <param name="keyword">Keyword to search with</param>
    /// <param name="urlToMatch">Results will be filtered with url. Returns all results if null or empty</param>
    public async Task<IEnumerable<UrlLocation>> SearchAndFilter(string keyword, string? urlToMatch, CancellationToken cancellationToken)
    {
        var query = new KeywordSearchQuery(keyword, 11);
        var keywordSearchResults = await _mediator.Send(query, cancellationToken);
        return KeywordsFilter.FilterSearchResultsByUrl(urlToMatch, keywordSearchResults);
    }
}