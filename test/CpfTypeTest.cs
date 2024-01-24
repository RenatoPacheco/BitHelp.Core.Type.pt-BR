using System;
using Xunit;

namespace BitHelp.Core.Type.pt_BR.Test
{
    public class CpfTypeTest
    {
        [Theory]
        [InlineData("153.179.966-35", "153.179.966-35")]
        [InlineData("15317996635", "153.179.966-35")]
        public void Check_format_is_valid(string input, string expected)
        {
            CpfType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = CpfType.Parse(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            input = test;
            Assert.Equal(expected, input);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        [InlineData("153.179.966-00")]
        [InlineData("15317996600")]
        public void Check_format_is_invalid(string input)
        {
            CpfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());

            test = input;
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        [InlineData("153.179.966-00")]
        [InlineData("15317996600")]
        public void Check_parse_exception(string input)
        {
            Assert.Throws<ArgumentException>(() => CpfType.Parse(input));
        }

        [Fact]
        public void Check_generate_is_valid()
        {
            string input = CpfType.Generate().ToString();
            CpfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("153.179.966-35", "D", "153.179.966-35")]
        [InlineData("153.179.966-35", "N", "15317996635")]
        [InlineData("153.179.966-35", "d", "153.179.966-35")]
        [InlineData("153.179.966-35", "n", "15317996635")]
        [InlineData("153.179.966-35", " D ", "153.179.966-35")]
        [InlineData("153.179.966-35", " N ", "15317996635")]
        [InlineData("153.179.966-35", " d ", "153.179.966-35")]
        [InlineData("153.179.966-35", " n ", "15317996635")]
        public void Check_to_string_is_valid(string input, string format, string expected)
        {
            CpfType test = new(input);
            Assert.Equal(expected, test.ToString(format));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("153.179.966-35", null)]
        [InlineData("153.179.966-35", "")]
        [InlineData("153.179.966-35", "   ")]
        [InlineData("153.179.966-35", "T")]
        [InlineData("153.179.966-35", "TD")]
        public void Check_to_string_exception(string input, string format)
        {
            CpfType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }
    }
}
