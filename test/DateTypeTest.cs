using Xunit;

namespace BitHelp.Core.Type.pt_BR.Test
{
    public class DateTypeTest
    {
        [Theory]
        [InlineData("31/12/2020", "31/12/2020")]
        [InlineData("31-12-2020", "31/12/2020")]
        [InlineData("1/1/2020", "01/01/2020")]
        public void Check_format_is_valid(string input, string expected)
        {
            DateType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("31 12 2020")]
        [InlineData("12/31/2020")]
        [InlineData("31/12/20")]
        public void Check_format_is_invalid(string input)
        {
            DateType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Fact]
        public void Check_string_null()
        {
            string input = null;
            DateType? test = input;

            Assert.Null(test);
        }

        [Fact]
        public void Check_string_empty()
        {
            string input = string.Empty;
            DateType test = input;

            Assert.Equal(string.Empty, test);
        }
    }
}
