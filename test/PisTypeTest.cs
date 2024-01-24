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
            Assert.True(test.IsValid());

            test = PisType.Parse(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            input = test;
            Assert.Equal(expected, input);
        }

        [Theory]
        [InlineData("497.79914.07-0")]
        [InlineData("49779914070")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_format_is_invalid(string input)
        {
            PisType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());

            test = input;
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("497.79914.07-0")]
        [InlineData("49779914070")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_parse_exception(string input)
        {
            Assert.Throws<ArgumentException>(() => PisType.Parse(input));
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
        [InlineData("497.79914.07-7", "D", "497.79914.07-7")]
        [InlineData("497.79914.07-7", "N", "49779914077")]
        [InlineData("497.79914.07-7", "d", "497.79914.07-7")]
        [InlineData("497.79914.07-7", "n", "49779914077")]
        [InlineData("497.79914.07-7", " D ", "497.79914.07-7")]
        [InlineData("497.79914.07-7", " N ", "49779914077")]
        [InlineData("497.79914.07-7", " d ", "497.79914.07-7")]
        [InlineData("497.79914.07-7", " n ", "49779914077")]
        public void Check_to_string_is_valid(string input, string format, string expected)
        {
            PisType test = new(input);
            Assert.Equal(expected, test.ToString(format));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("497.79914.07-7", null)]
        [InlineData("497.79914.07-7", "")]
        [InlineData("497.79914.07-7", "   ")]
        [InlineData("497.79914.07-7", "T")]
        [InlineData("497.79914.07-7", "TD")]
        public void Check_to_string_exception(string input, string format)
        {
            PisType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }
    }
}
