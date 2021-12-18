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
        }

        [Theory]
        [InlineData("20.024.269/0001-00")]
        public void Check_format_is_invalid(string input)
        {
            CnpjType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }
    }
}
