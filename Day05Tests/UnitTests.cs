using Day05;
using Xunit;

namespace Day05Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData('a', 'A', true)]
        [InlineData('a', 'a', false)]
        [InlineData('a', 'b', false)]
        [InlineData('a', 'B', false)]
        [InlineData('A', 'A', false)]
        [InlineData('A', 'B', false)]
        public void ReactsWithReturnsCorrectly(char a, char b, bool expected)
        {
            var subject = new Unit(a);
            var candidate = new Unit(b);
            var actual = subject.ReactsWith(candidate);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('a', 'a', true)]
        [InlineData('a', 'A', true)]
        [InlineData('A', 'A', true)]
        [InlineData('a', 'b', false)]
        [InlineData('a', 'B', false)]
        [InlineData('A', 'b', false)]
        [InlineData('A', 'B', false)]
        public void EqualsIgnorePolarityReturnsCorrectly(char a, char b, bool expected)
        {
            var subject = new Unit(a);
            var candidate = new Unit(b);
            var actual = subject.EqualsIgnorePolarity(candidate);
            Assert.Equal(expected, actual);
        }
    }
}
