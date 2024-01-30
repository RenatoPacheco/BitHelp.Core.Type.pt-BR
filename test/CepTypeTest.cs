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
            Assert.True(test.IsValid());

            test = CepType.Parse(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            input = test;
            Assert.Equal(expected, input);
        }

        [Theory]
        [InlineData("08090-28")]
        [InlineData("0809028")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_format_is_invalid(string input)
        {
            CepType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());

            test = input;
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("08090-28")]
        [InlineData("0809028")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_parse_exception(string input)
        {
            Assert.Throws<ArgumentException>(() => CepType.Parse(input));
        }

        [Theory]
        [InlineData("08090-284", "D", "08090-284")]
        [InlineData("08090-284", "N", "08090284")]
        [InlineData("08090-284", "d", "08090-284")]
        [InlineData("08090-284", "n", "08090284")]
        [InlineData("08090-284", " D ", "08090-284")]
        [InlineData("08090-284", " N ", "08090284")]
        [InlineData("08090-284", " d ", "08090-284")]
        [InlineData("08090-284", " n ", "08090284")]
        public void Check_to_string_is_valid(string input, string format, string expected)
        {
            CepType test = new(input);
            Assert.Equal(expected, test.ToString(format));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("08090-284", null)]
        [InlineData("08090-284", "")]
        [InlineData("08090-284", "   ")]
        [InlineData("08090-284", "T")]
        [InlineData("08090-284", "TD")]
        public void Check_to_string_exception(string input, string format)
        {
            CepType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }
    }
}
