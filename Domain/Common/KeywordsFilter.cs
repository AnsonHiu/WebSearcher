using Domain.Entities;
using Domain.Queries.SearchQuery;

namespace Domain.Services;
public class KeywordsFilter
{
    public static IEnumerable<MatchedUrlLocation> GetMatchedUrl(string urlToMatch, IEnumerable<KeywordSearchQueryResult> searchQueryResults)
    {
        if(string.IsNullOrWhiteSpace(urlToMatch))
        {
            return searchQueryResults.Select((searchResult, index) => new MatchedUrlLocation(searchResult.FullUrl, index));
        }
        var matchedResults = new List<MatchedUrlLocation>();
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