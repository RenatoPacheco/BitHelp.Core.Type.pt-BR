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
        }

        [Theory]
        [InlineData("M1")]
        [InlineData("M G")]
        public void Check_format_is_invalid(string input)
        {
            UfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }
    }
}
