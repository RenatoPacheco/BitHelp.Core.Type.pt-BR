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
            Assert.True(test.IsValid());

            test = CnpjType.Parse(input);
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
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("20.024.269/0001-00")]
        public void Check_format_is_invalid(string input)
        {
            CnpjType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());

            test = input;
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("20.024.269/0001-00")]
        public void Check_parse_exception(string input)
        {
            Assert.Throws<ArgumentException>(() => CnpjType.Parse(input));
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
        [InlineData("00.038.166/0001-05", "D", "00.038.166/0001-05")]
        [InlineData("00.038.166/0001-05", "N", "00038166000105")]
        [InlineData("00.038.166/0001-05", "d", "00.038.166/0001-05")]
        [InlineData("00.038.166/0001-05", "n", "00038166000105")]
        [InlineData("00.038.166/0001-05", " D ", "00.038.166/0001-05")]
        [InlineData("00.038.166/0001-05", " N ", "00038166000105")]
        [InlineData("00.038.166/0001-05", " d ", "00.038.166/0001-05")]
        [InlineData("00.038.166/0001-05", " n ", "00038166000105")]
        public void Check_to_string_is_valid(string input, string format, string expected)
        {
            CnpjType test = new(input);
            Assert.Equal(expected, test.ToString(format));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("20.024.269/0001-00", null)]
        [InlineData("20.024.269/0001-00", "")]
        [InlineData("20.024.269/0001-00", "   ")]
        [InlineData("20.024.269/0001-00", "T")]
        [InlineData("20.024.269/0001-00", "TD")]
        public void Check_to_string_exception(string input, string format)
        {
            CnpjType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }
    }
}
