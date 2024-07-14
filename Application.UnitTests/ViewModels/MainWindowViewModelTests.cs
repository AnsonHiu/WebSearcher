using Application.Common.Mappings;
using Application.Models;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

using DomainUrlLocation = Domain.Entities.UrlLocation;

namespace Application.UnitTests.ViewModels;

public class MainWindowViewModelTests
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly Mock<ISearchAndFilterService> _searchAndFilterServiceMock;
    private readonly IMapper _mapper;

    public MainWindowViewModelTests()
    {
        _searchAndFilterServiceMock = new Mock<ISearchAndFilterService>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ApplicationMapping>()));
        _mainWindowViewModel = new(_searchAndFilterServiceMock.Object, _mapper);
    }

    [Fact]
    public async Task GivenViewModel_SearchForKeywords_ServiceIsCalled()
    {
        // Arrange
        const string keyword = "conveyancing software";
        const string urlToMatch = "www.smokeball.com.au";
        _mainWindowViewModel.TxtMatchingUrl = urlToMatch;
        _mainWindowViewModel.TxtKeyword = keyword;
        IEnumerable<DomainUrlLocation> urlLocations = [new("Test1", 1), new("Test2", 2)];
        _searchAndFilterServiceMock.Setup(mock => mock.SearchAndFilter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(urlLocations);
        var expectedResult = _mapper.Map<IEnumerable<UrlLocation>>(urlLocations);

        // Act
        await _mainWindowViewModel.SearchForKeywords();

        // Assert
        _searchAndFilterServiceMock.Verify(mock => mock.SearchAndFilter(
            It.Is<string>(actualKeyword => actualKeyword == keyword),
            It.Is<string>(actualUrlToMatch => actualUrlToMatch == urlToMatch),
            It.IsAny<CancellationToken>()), Times.Once);
        _mainWindowViewModel.SearchResults.ToList().Should().BeEquivalentTo(expectedResult);
    }
}
