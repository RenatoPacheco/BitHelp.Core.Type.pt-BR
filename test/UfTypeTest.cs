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
            Assert.True(test.IsValid());

            test = UfType.Parse(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            input = test;
            Assert.Equal(expected, input);
        }

        [Theory]
        [InlineData("M1")]
        [InlineData("M G")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_format_is_invalid(string input)
        {
            UfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());

            test = input;
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("M1")]
        [InlineData("M G")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_parse_exception(string input)
        {
            Assert.Throws<ArgumentException>(() => UfType.Parse(input));
        }
    }
}
