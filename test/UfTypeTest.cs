using Xunit;

namespace BitHelp.Core.Type.pt_BR.Test
{
    public class UfTypeTest
    {
        [Theory]
        [InlineData("MG", "MG")]
        [InlineData("mg", "MG")]
        public void Check_format_is_valid(string input, string expected)
        {
            UfType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("M1")]
        [InlineData("M G")]
        public void Check_format_is_invalid(string input)
        {
            UfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_null(string value)
        {
            UfType? test = value;
            Assert.Equal(value, test?.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_empty(string value)
        {
            UfType test = value;

            Assert.Equal(string.Empty, test.ToString());
        }
    }
}
