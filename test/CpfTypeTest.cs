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
        }

        [Theory]
        [InlineData("153.179.966-00")]
        [InlineData("15317996600")]
        public void Check_format_is_invalid(string input)
        {
            CpfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }
    }
}
