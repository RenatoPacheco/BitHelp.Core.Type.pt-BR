using Microsoft.VisualBasic;
using System;
using Xunit;

namespace BitHelp.Core.Type.pt_BR.Test
{
    public class CepTypeTest
    {
        [Theory]
        [InlineData("08090-284", "08090-284")]
        [InlineData("08090284", "08090-284")]
        public void Check_format_is_valid(string input, string expected)
        {
            CepType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.Equal(expected, Convert.ToString(test));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("08090-28")]
        [InlineData("0809028")]
        public void Check_format_is_invalid(string input)
        {
            CepType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_null(string value)
        {
            CepType? test = value;
            Assert.Equal(value, test?.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_empty(string value)
        {
            CepType test = value;

            Assert.Equal(string.Empty, test.ToString());
        }

        [Fact]
        public void Check_try_parse_valid()
        {
            string value = "08090-284";
            Assert.True(CepType.TryParse(value, out CepType result));
            Assert.Equal(value, result.ToString());
        }

        [Fact]
        public void Check_try_parse_invalid()
        {
            string value = "08090-28";
            Assert.False(CepType.TryParse(value, out CepType result));
            Assert.Equal(CepType.Empty.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("0809028")]
        public void Check_parse_invalid(string value)
        {
            Assert.Throws<ArgumentException>(() => CepType.Parse(value));
        }

        [Fact]
        public void Check_parse_valid()
        {
            string value = "08090-284";
            CepType result = CepType.Parse(value);
            Assert.Equal(value, result.ToString());
        }
    }
}
