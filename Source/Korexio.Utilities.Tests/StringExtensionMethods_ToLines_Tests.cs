using Xunit;

namespace Korexio.Utilities.Tests;

public class StringExtensionMethods_ToLines_Tests
{
    [Theory]
    [InlineData("Line 1\rLine 2")]
    [InlineData("Line 1\nLine 2")]
    [InlineData("Line 1\r\nLine 2")]
    [InlineData("Line 1\n\rLine 2")]
    [InlineData("Line 1\fLine 2")]
    [InlineData("Line 1\u0085Line 2")]
    [InlineData("Line 1\u2028Line 2")]
    [InlineData("Line 1\u2029Line 2")]
    public void Two_Lines_Separated_By_One_Line_Separator_Are_Processed_Correctly(string value)
    {
        // Act

        var lines = value.ToLines();

        // Assert

        Assert.Equal(new[] { "Line 1", "Line 2" }, lines);
    }

    [Fact]
    public void Multiple_Lines_Are_Processed_Correctly()
    {
        // Arrange

        const string value = "Line 1\rLine 2\nLine 3\r\nLine 4\n\rLine 5\fLine 6\u0085Line 7\u2028Line 8\u2029Line 9";

        // Act

        var lines = value.ToLines();

        // Assert

        Assert.Equal(new[] { "Line 1", "Line 2", "Line 3", "Line 4", "Line 5", "Line 6", "Line 7", "Line 8", "Line 9" }, lines);
    }

    [Fact]
    public void Empty_Lines_Are_Processed_Correctly()
    {
        // Arrange

        const string value = "\r\n\n\r\f\u0085\u2028\u2029";

        // Act

        var lines = value.ToLines();

        // Assert

        Assert.Equal(7, lines.Length);
        Assert.All(lines, Assert.Empty);
    }

    [Fact]
    public void ArgumentNullException_Is_Thrown_If_Value_Is_Null()
    {
        // Arrange

        const string? value = null;

        // Act & Assert

        _ = Assert.Throws<ArgumentNullException>(() => value!.ToLines());
    }
}
