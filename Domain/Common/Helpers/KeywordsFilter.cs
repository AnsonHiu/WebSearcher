using Domain.Entities;
using Domain.Queries.SearchQuery;

namespace Domain.Common.Helpers;

public class KeywordsFilter
{
    /// <summary>
    /// Filter that leaves only items with a matching base url
    /// </summary>
    public static IEnumerable<UrlLocation> FilterSearchResultsByUrl(string? urlToMatch, IEnumerable<KeywordSearchQueryResult> searchQueryResults)
    {
        var count = 0;
        var searchResultsEnumerator = searchQueryResults.GetEnumerator();
        while (searchResultsEnumerator.MoveNext())
        {
            if (searchResultsEnumerator.Current.BaseUrl.Equals(urlToMatch, StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(urlToMatch))
            {
                yield return new(searchResultsEnumerator.Current.FullUrl, count);
            }
            count++;
        }
    }
}