using AutoMapper;
using Data.Interfaces;
using Data.Models.SearchParams;
using Domain.Common.Mappings;
using Domain.Queries.SearchQuery;
using FluentAssertions;
using Google.Apis.CustomSearchAPI.v1.Data;
using Moq;

namespace Domain.UnitTests.Queries.KeywordSearch;

public class KeywordSearchQueryHandlerTests
{
    private readonly KeywordSearchQueryHandler _keywordSearchQueryHandler;
    private readonly Mock<ISearchService> _searchServiceMock;
    private readonly IMapper _mapper;

    public KeywordSearchQueryHandlerTests()
    {
        _searchServiceMock = new Mock<ISearchService>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<DomainMapping>()));
        var validator = new KeywordSearchQueryValidator();
        _keywordSearchQueryHandler = new KeywordSearchQueryHandler(_searchServiceMock.Object, _mapper, validator);
    }

    [Fact]
    public async Task GivenKeywordSearchQuery_WhenHandled_ReturnsResults()
    {
        // Arrange
        var returnedResults = new List<Result> 
        {
            new()
            {
                DisplayLink = "baseUrl",
                Link = "fullUrl"
            }
        };
        _searchServiceMock.Setup(mock => mock.Search(It.IsAny<SearchParams>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(returnedResults);
        const string keyword = "test";
        const int count = 100;
        var query = new KeywordSearchQuery(keyword, count);
        var expectedReturnedResults = _mapper.Map<IEnumerable<KeywordSearchQueryResult>>(returnedResults);

        // Act
        var results = await _keywordSearchQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        _searchServiceMock.Verify(
            mock => mock.Search(It.Is<SearchParams>(p => p.Keywords == keyword && p.ReturnCount == count), It.IsAny<CancellationToken>()),
            Times.Once);
        results.Should().BeEquivalentTo(expectedReturnedResults);
    }
}
