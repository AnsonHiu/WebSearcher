using Data.Models.SearchParams;
using FluentValidation.TestHelper;

namespace Data.UnitTests.Models;

public class SearchParamsValidatorTests
{

    private readonly SearchParamsValidator _validator;

    public SearchParamsValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public void GivenReturnCountOver100_WhenValidating_ReturnsError(int returnCount)
    {
        // Arrange
        var searchParams = new SearchParams("some keywords", returnCount);

        // Act
        var result = _validator.TestValidate(searchParams);

        // Assert
        result.ShouldHaveValidationErrorFor(searchParam => searchParam.ReturnCount);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void GivenReturnCountWithin0To100_WhenValidating_ReturnsSuccess(int returnCount)
    {
        // Arrange
        var searchParams = new SearchParams("some keywords", returnCount);

        // Act
        var results = _validator.TestValidate(searchParams);

        // Assert
        results.ShouldNotHaveValidationErrorFor(searchParam => searchParam.ReturnCount);
    }
}
