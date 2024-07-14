using Domain.Common.Helpers;
using Domain.Queries.SearchQuery;
using FluentAssertions;

namespace Domain.UnitTests.Common.Helpers;

public class KeywordsFilterTests
{
    [Fact]
    public void GivenUrlToMatchAndListOfResults_WhenFiltered_ReturnsOnlyMatchingResults()
    {
        // Arrange
        const string urlToFilter = "test";
        IEnumerable<KeywordSearchQueryResult> matchedResults = [
            new("TEST", "fullUrl1"),
            new("TeSt", "fullUrl2")
        ];
        IEnumerable<KeywordSearchQueryResult> unmatchedResults = [
            new("notAFullMatchTest", "fullUrl1"),
            new("noMatch", "fullUrl2")
        ];

        // Act
        var result = KeywordsFilter.FilterSearchResultsByUrl(urlToFilter, [.. matchedResults, .. unmatchedResults]);

        // Assert
        result.Select(location => location.FullUrl).Should().BeEquivalentTo(matchedResults.Select(match => match.FullUrl));
    }
}
