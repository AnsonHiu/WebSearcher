using Domain.Entities;
using Domain.Queries.SearchQuery;
using Domain.Services;
using FluentAssertions;
using MediatR;
using Moq;

namespace Domain.UnitTests.Services;

public class KeywordSearchAndFilterServiceTests
{
    private readonly KeywordSearchAndFilterService _keywordSearchAndFilterService;
    private readonly Mock<IMediator> _mediatorMock;

    public KeywordSearchAndFilterServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _keywordSearchAndFilterService = new(_mediatorMock.Object);
    }

    [Fact]
    public async Task GivenKeywordAndUrlToMatch_WhenSearchAndFiltered_ReturnsResults()
    {
        // Arrange
        IEnumerable<KeywordSearchQueryResult> keywordSearchQueryResults = [
            new("baseUrl1", "fullUrl1"),
            new("baseUrl2", "fullUrl2")
        ];
        _mediatorMock.Setup(mock => mock.Send(It.IsAny<KeywordSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(keywordSearchQueryResults);
        const string keyword = "test";
        var expectedResults = keywordSearchQueryResults.Select((searchResult, index) => new UrlLocation(searchResult.FullUrl, index));

        // Act
        var result = await _keywordSearchAndFilterService.SearchAndFilter(keyword, null, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResults);
        _mediatorMock.Verify(mock => mock.Send(It.Is<KeywordSearchQuery>(query => query.Keywords == keyword), It.IsAny<CancellationToken>()), Times.Once);
    }
}
