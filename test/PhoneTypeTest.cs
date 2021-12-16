using Xunit;

namespace BitHelp.Core.Type.pt_br.Test
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
        }

        [Theory]
        [InlineData("11312345678")]
        [InlineData("111234567")]
        [InlineData("011312345678")]
        [InlineData("0111234567")]
        [InlineData("312345678")]
        [InlineData("1234567")]
        public void Check_format_is_invalid(string input)
        {
            PhoneType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }
    }
}
