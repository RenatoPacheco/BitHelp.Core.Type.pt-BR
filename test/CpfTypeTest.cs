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
            Assert.Equal(expected, Convert.ToString(test));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("153.179.966-00")]
        [InlineData("15317996600")]
        public void Check_format_is_invalid(string input)
        {
            CpfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
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
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_null(string value)
        {
            CpfType? test = value;
            Assert.Equal(value, test?.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_empty(string value)
        {
            CpfType test = value;

            Assert.Equal(string.Empty, test.ToString());
        }

        [Fact]
        public void Check_try_parse_valid()
        {
            string value = "153.179.966-35";
            Assert.True(CpfType.TryParse(value, out CpfType result));
            Assert.Equal(value, result.ToString());
        }

        [Fact]
        public void Check_try_parse_invalid()
        {
            string value = "153.179.966-00";
            Assert.False(CpfType.TryParse(value, out CpfType result));
            Assert.Equal(CpfType.Empty.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("153.179.966-00")]
        public void Check_parse_invalid(string value)
        {
            Assert.Throws<ArgumentException>(() => CpfType.Parse(value));
        }

        [Fact]
        public void Check_parse_valid()
        {
            string value = "153.179.966-35";
            CpfType result = CpfType.Parse(value);
            Assert.Equal(value, result.ToString());
        }
    }
}
