using Day09;
using Xunit;

namespace Day09Tests
{
    public class MarbleManiaTests
    {
        [Theory]
        [InlineData(9, 25, 32)]
        [InlineData(10, 1618, 8317)]
        [InlineData(13, 7999, 146373)]
        [InlineData(17, 1104, 2764)]
        [InlineData(21, 6111, 54718)]
        [InlineData(30, 5807, 37305)]
        public void PlayCompletesCorrectly(int playerCount, int lastMarble, long expected)
        {
            var subject = new MarbleMania(playerCount, lastMarble);
            subject.Play();
            Assert.Equal(expected, subject.HighestScore);
        }
    }
}
