using Domain.Entities;
using Domain.Queries.SearchQuery;

namespace Domain.Services;
public class KeywordsFilter
{
    public static IEnumerable<UrlLocation> FilterSearchResultsByUrl(string? urlToMatch, IEnumerable<KeywordSearchQueryResult> searchQueryResults)
    {
        // TODO: implement deferred execution to chain return results
        if(string.IsNullOrWhiteSpace(urlToMatch))
        {
            return searchQueryResults.Select((searchResult, index) => new UrlLocation(searchResult.FullUrl, index));
        }
        var matchedResults = new List<UrlLocation>();
        var count = 0;
        var searchResultsEnumerator = searchQueryResults.GetEnumerator();
        while (searchResultsEnumerator.MoveNext())
        {
            if (searchResultsEnumerator.Current.BaseUrl.Equals(urlToMatch, StringComparison.OrdinalIgnoreCase))
            {
                matchedResults.Add(new(searchResultsEnumerator.Current.FullUrl, count));
            }
            count++;
        }
        return matchedResults;
    }
}