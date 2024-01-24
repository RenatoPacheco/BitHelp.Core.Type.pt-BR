using System;
using Xunit;

namespace BitHelp.Core.Type.pt_BR.Test
{
    public class PhoneTypeTest
    {
        [Theory]
        [InlineData("11912345678", "(11) 91234-5678")]
        [InlineData("1112345678", "(11) 1234-5678")]
        [InlineData("011912345678", "(11) 91234-5678")]
        [InlineData("01112345678", "(11) 1234-5678")]
        [InlineData("912345678", "91234-5678")]
        [InlineData("12345678", "1234-5678")]
        // ----------------------------------------
        [InlineData("11 9 1234 5678", "(11) 91234-5678")]
        [InlineData("11 1234 5678", "(11) 1234-5678")]
        [InlineData("011 9 1234 5678", "(11) 91234-5678")]
        [InlineData("011 1234 5678", "(11) 1234-5678")]
        [InlineData("9 1234 5678", "91234-5678")]
        [InlineData("1234 5678", "1234-5678")]
        // ----------------------------------------
        [InlineData("(11) 91234-5678", "(11) 91234-5678")]
        [InlineData("(11) 1234-5678", "(11) 1234-5678")]
        [InlineData("(011) 91234-5678", "(11) 91234-5678")]
        [InlineData("(011) 1234-5678", "(11) 1234-5678")]
        [InlineData("91234-5678", "91234-5678")]
        [InlineData("1234-5678", "1234-5678")]
        public void Check_format_is_valid(string input, string expected)
        {
            PhoneType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = PhoneType.Parse(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            test = input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());

            input = test;
            Assert.Equal(expected, input);
        }

        [Theory]
        [InlineData("11312345678")]
        [InlineData("111234567")]
        [InlineData("011312345678")]
        [InlineData("0111234567")]
        [InlineData("312345678")]
        [InlineData("1234567")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_format_is_invalid(string input)
        {
            PhoneType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());

            test = input;
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("11312345678")]
        [InlineData("111234567")]
        [InlineData("011312345678")]
        [InlineData("0111234567")]
        [InlineData("312345678")]
        [InlineData("1234567")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Check_parse_exception(string input)
        {
            Assert.Throws<ArgumentException>(() => PhoneType.Parse(input));
        }

        [Theory]
        [InlineData("11912345678", "D", "(11) 91234-5678")]
        [InlineData("11912345678", "N", "11912345678")]
        [InlineData("11912345678", "d", "(11) 91234-5678")]
        [InlineData("11912345678", "n", "11912345678")]
        [InlineData("11912345678", " D ", "(11) 91234-5678")]
        [InlineData("11912345678", " N ", "11912345678")]
        [InlineData("11912345678", " d ", "(11) 91234-5678")]
        [InlineData("11912345678", " n ", "11912345678")]
        public void Check_to_string_is_valid(string input, string format, string expected)
        {
            PhoneType test = new(input);
            Assert.Equal(expected, test.ToString(format));
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("11912345678", null)]
        [InlineData("11912345678", "")]
        [InlineData("11912345678", "   ")]
        [InlineData("11912345678", "T")]
        [InlineData("11912345678", "TD")]
        public void Check_to_string_exception(string input, string format)
        {
            PhoneType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }
    }
}
