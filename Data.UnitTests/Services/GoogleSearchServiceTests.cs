using Data.Interfaces;
using Data.Models;
using Data.Models.SearchParams;
using Data.Services.Google;
using FluentAssertions;
using FluentValidation;
using Google;
using Microsoft.Extensions.Logging;
using Moq;
using Search = Google.Apis.CustomSearchAPI.v1.Data.Search;

namespace Data.UnitTests.Services;

public class GoogleSearchServiceTests
{
    private readonly GoogleSearchService _googleSearchService;
    private readonly Mock<IGoogleApiService> _googleApiServiceMock;
    private readonly Mock<ILogger<GoogleSearchService>> _loggerMock;

    public GoogleSearchServiceTests()
    {
        _loggerMock = new Mock<ILogger<GoogleSearchService>>();
        _googleApiServiceMock = new Mock<IGoogleApiService>();
        var validator = new SearchParamsValidator();
        _googleSearchService = new(_loggerMock.Object, _googleApiServiceMock.Object, validator);
    }

    [Theory]
    [InlineData(20)]
    [InlineData(31)]
    [InlineData(25)]
    [InlineData(26)]
    [InlineData(29)]
    public async Task GivenSearchParams_WhenQueryApi_ReturnsResult(int returnCount)
    {
        // Arrange
        _googleApiServiceMock.Setup(mock => mock.ExecuteSearchAsync(It.IsAny<GoogleSearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Search() { Items = [] });
        var keyword = "conveyance software";
        var count = returnCount;
        var searchParams = new SearchParams(keyword, count);
        var expectedExecutionTimes = (int)Math.Ceiling((double)returnCount / GoogleSearchService.DefaultPageSize);

        // Act
        var result = await _googleSearchService.Search(searchParams, CancellationToken.None);

        // Assert
        _googleApiServiceMock.Verify(
            mock => mock.ExecuteSearchAsync(It.Is<GoogleSearchRequest>(request => request.Keyword == keyword), It.IsAny<CancellationToken>()),
            Times.Exactly(expectedExecutionTimes));
    }

    [Fact]
    public async Task GivenInvalidSearchParams_WhenQueryApi_ThrowsException()
    {
        // Arrange
        var searchParams = new SearchParams("some keyword", 101);

        // Act
        Func<Task> searchAction = () => _googleSearchService.Search(searchParams, CancellationToken.None);

        // Assert
        await searchAction.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task GivenSearchParams_IfGoogleApiThrowsError_LogsError()
    {
        // Arrange
        _googleApiServiceMock.Setup(mock => mock.ExecuteSearchAsync(It.IsAny<GoogleSearchRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new GoogleApiException("customApiService"));
        var searchParams = new SearchParams("some keyword", 5);

        // Act
        Func<Task> searchAction = () => _googleSearchService.Search(searchParams, CancellationToken.None);

        // Assert
        await searchAction.Should().ThrowAsync<GoogleApiException>();
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.Is<EventId>(eventId => eventId.Id == 0),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
        Times.Once);

    }
}