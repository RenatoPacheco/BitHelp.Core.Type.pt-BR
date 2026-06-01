using System;
using Xunit;

namespace BitHelp.Core.Type.pt_BR.Test
{
    public class PisTypeTest
    {
        [Theory]
        [InlineData("497.79914.07-7", "497.79914.07-7")]
        [InlineData("49779914077", "497.79914.07-7")]
        public void Check_format_is_valid(string input, string expected)
        {
            PisType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.Equal(expected, Convert.ToString(test));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("497.79914.07-0")]
        [InlineData("49779914070")]
        public void Check_format_is_invalid(string input)
        {
            PisType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Fact]
        public void Check_generate_is_valid()
        {
            string input = PisType.Generate().ToString();
            PisType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_null(string value)
        {
            PisType? test = value;
            Assert.Equal(value, test?.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_empty(string value)
        {
            PisType test = value;

            Assert.Equal(string.Empty, test.ToString());
        }

        [Fact]
        public void Check_try_parse_valid()
        {
            string value = "497.79914.07-7";
            Assert.True(PisType.TryParse(value, out PisType result));
            Assert.Equal(value, result.ToString());
        }

        [Fact]
        public void Check_try_parse_invalid()
        {
            string value = "497.79914.07-0";
            Assert.False(PisType.TryParse(value, out PisType result));
            Assert.Equal(PisType.Empty.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("497.79914.07-0")]
        public void Check_parse_invalid(string value)
        {
            Assert.Throws<ArgumentException>(() => PisType.Parse(value));
        }

        [Fact]
        public void Check_parse_valid()
        {
            string value = "497.79914.07-7";
            PisType result = PisType.Parse(value);
            Assert.Equal(value, result.ToString());
        }
    }
}
