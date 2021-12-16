using Xunit;

namespace BitHelp.Core.Type.pt_br.Test
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
        }

        [Theory]
        [InlineData("497.79914.07-0")]
        [InlineData("49779914070")]
        public void Check_format_is_invalid(string input)
        {
            PisType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }
    }
}
