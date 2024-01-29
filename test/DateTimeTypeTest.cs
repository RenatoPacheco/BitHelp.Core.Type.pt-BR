using System;
using Xunit;

namespace BitHelp.Core.Type.pt_BR.Test
{
    public class DateTimeTypeTest
    {
        [Theory]
        [InlineData("31/12/2020 12:34:56", "31/12/2020 12:34:56")]
        [InlineData("31-12-2020 12:34:56", "31/12/2020 12:34:56")]
        [InlineData("1/1/2020 12:34:56", "01/01/2020 12:34:56")]
        [InlineData("1/1/2020", "01/01/2020 00:00:00")]
        [InlineData("1/1/2020 12:00", "01/01/2020 12:00:00")]
        public void Check_format_is_valid(string input, string expected)
        {
            DateTimeType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = DateTimeType.Parse(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            input = test;
            Assert.Equal(expected, input);
        }

        [Theory]
        [InlineData("31 12 2020")]
        [InlineData("12/31/2020")]
        [InlineData("31/12/20")]
        public void Check_format_is_invalid(string input)
        {
            DateTimeType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());

            test = input;
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("31 12 2020")]
        [InlineData("12/31/2020")]
        [InlineData("31/12/20")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_parse_exception(string input)
        {
            Assert.Throws<ArgumentException>(() => DateTimeType.Parse(input));
        }
    }
}
