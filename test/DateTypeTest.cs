using System;
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
            Assert.Equal(expected, Convert.ToString(test));
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

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_null(string value)
        {
            DateType? test = value;
            Assert.Equal(value, test?.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_empty(string value)
        {
            DateType test = value;

            Assert.Equal(string.Empty, test.ToString());
        }

        [Fact]
        public void Check_try_parse_valid()
        {
            string value = "31/12/2020";
            Assert.True(DateType.TryParse(value, out DateType result));
            Assert.Equal(value, result.ToString());
        }

        [Fact]
        public void Check_try_parse_invalid()
        {
            string value = "31 12 2020";
            Assert.False(DateType.TryParse(value, out DateType result));
            Assert.Equal(DateType.Empty.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("31 12 2020")]
        public void Check_parse_invalid(string value)
        {
            Assert.Throws<ArgumentException>(() => DateType.Parse(value));
        }

        [Fact]
        public void Check_parse_valid()
        {
            string value = "31/12/2020";
            DateType result = DateType.Parse(value);
            Assert.Equal(value, result.ToString());
        }
    }
}
