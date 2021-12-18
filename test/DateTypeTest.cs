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
    }
}
