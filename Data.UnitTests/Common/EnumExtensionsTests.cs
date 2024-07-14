using FluentAssertions;
using System.ComponentModel;
using static Data.Common.Extensions.EnumExtensions;

namespace Data.UnitTests.Common;

public class EnumExtensionsTests
{
    private const string Value1Description = "DescriptionOfValue1";
    private const string Value2Description = "";

    [Theory]
    [InlineData(TestEnum.Value1, Value1Description)]
    [InlineData(TestEnum.Value2, Value2Description)]
    [InlineData(TestEnum.Value3, null)]
    public void GivenEnum_WhenGetDescription_ReturnsExpectedResult(TestEnum enumValue, string? expectedEnumDescription)
    {
        // Act
        var result = enumValue.GetDescription();

        // Assert
        result.Should().Be(expectedEnumDescription);
    }

    public enum TestEnum
    {
        [Description(Value1Description)]
        Value1,
        [Description(Value2Description)]
        Value2,
        Value3
    }
}