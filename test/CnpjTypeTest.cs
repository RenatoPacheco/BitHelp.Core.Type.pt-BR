using System;
using Xunit;

namespace BitHelp.Core.Type.pt_BR.Test
{
    public class CnpjTypeTest
    {
        [Theory]
        [InlineData("00.038.166/0001-05", "00.038.166/0001-05")]
        [InlineData("00038166000105", "00.038.166/0001-05")]
        public void Check_format_is_valid(string input, string expected)
        {
            CnpjType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.Equal(expected, Convert.ToString(test));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("20.024.269/0001-00")]
        public void Check_format_is_invalid(string input)
        {
            CnpjType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Fact]
        public void Check_generate_is_valid()
        {
            string input = CnpjType.Generate().ToString();
            CnpjType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_null(string value)
        {
            CnpjType? test = value;
            Assert.Equal(value, test?.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Check_string_empty(string value)
        {
            CnpjType test = value;

            Assert.Equal(string.Empty, test.ToString());
        }

        [Fact]
        public void Check_try_parse_valid()
        {
            string value = "00.038.166/0001-05";
            Assert.True(CnpjType.TryParse(value, out CnpjType result));
            Assert.Equal(value, result.ToString());
        }

        [Fact]
        public void Check_try_parse_invalid()
        {
            string value = "20.024.269/0001-00";
            Assert.False(CnpjType.TryParse(value, out CnpjType result));
            Assert.Equal(CnpjType.Empty.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("20.024.269/0001-00")]
        public void Check_parse_invalid(string value)
        {
            Assert.Throws<ArgumentException>(() => CnpjType.Parse(value));
        }

        [Fact]
        public void Check_parse_valid()
        {
            string value = "00.038.166/0001-05";
            CnpjType result = CnpjType.Parse(value);
            Assert.Equal(value, result.ToString());
        }
    }
}
