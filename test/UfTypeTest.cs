using System;
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
            Assert.Equal(expected, Convert.ToString(test));
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

        [Fact]
        public void Check_try_parse_valid()
        {
            string value = "MG";
            Assert.True(UfType.TryParse(value, out UfType result));
            Assert.Equal(value, result.ToString());
        }

        [Fact]
        public void Check_try_parse_invalid()
        {
            string value = "M1";
            Assert.False(UfType.TryParse(value, out UfType result));
            Assert.Equal(UfType.Empty.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("M1")]
        public void Check_parse_invalid(string value)
        {
            Assert.Throws<ArgumentException>(() => UfType.Parse(value, out UfType result));
        }

        [Fact]
        public void Check_parse_valid()
        {
            string value = "MG";
            UfType.Parse(value, out UfType result);
            Assert.Equal(value, result.ToString());
        }
    }
}
