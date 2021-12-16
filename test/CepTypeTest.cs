using Xunit;

namespace BitHelp.Core.Type.pt_br.Test
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
        }

        [Theory]
        [InlineData("08090-28")]
        [InlineData("0809028")]
        public void Check_format_is_invalid(string input)
        {
            CepType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }
    }
}
