using Day09;
using Xunit;

namespace Day09Tests
{
    public class MarbleTests
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(22, false)]
        [InlineData(23, true)]
        [InlineData(24, false)]
        [InlineData(46, true)]
        public void CountsReturnsCorrectly(int value, bool expected)
        {
            var subject = new Marble(value);
            Assert.Equal(expected, subject.Counts());
        }
    }
}
