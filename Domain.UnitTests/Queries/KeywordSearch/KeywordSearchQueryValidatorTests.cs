using Domain.Queries.SearchQuery;
using FluentValidation.TestHelper;

namespace Domain.UnitTests.Queries.KeywordSearch;

public class KeywordSearchQueryValidatorTests
{
    private readonly KeywordSearchQueryValidator _validator;

    public KeywordSearchQueryValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void GivenInvalidKeywords_WhenValidating_ReturnsError(string? keywords)
    {
        // Arrange
        var keywordSearchQuery = new KeywordSearchQuery(keywords, 50);

        // Act
        var result = _validator.TestValidate(keywordSearchQuery);

        // Assert
        result.ShouldHaveValidationErrorFor(query => query.Keywords);
    }

    [Fact]
    public void GivenValidKeywords_WhenValidating_ReturnsSuccess()
    {
        // Arrange
        var keywordSearchQuery = new KeywordSearchQuery("test", 50);

        // Act
        var result = _validator.TestValidate(keywordSearchQuery);

        // Assert
        result.ShouldNotHaveValidationErrorFor(query => query.Keywords);
    }
}
