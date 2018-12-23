using System.Linq;
using Day13;
using Xunit;

namespace Day13Tests
{
    public class CoordinatesTests
    {
        [Fact]
        public void CoordinatesAreSortedCorrectly()
        {
            var subject = new[]
            {
                new Coordinates(1, 2),
                new Coordinates(4, 3),
                new Coordinates(1, 1),
                new Coordinates(1, 3),
                new Coordinates(2, 1),
                new Coordinates(3, 2),
                new Coordinates(4, 1),
            };

            var expected = new[]
            {
                new Coordinates(1, 1),
                new Coordinates(1, 2),
                new Coordinates(1, 3),
                new Coordinates(2, 1),
                new Coordinates(3, 2),
                new Coordinates(4, 1),
                new Coordinates(4, 3),
            };

            var actual = subject.OrderBy(c => c).ToArray();
            Assert.Equal(expected, actual);
        }
    }
}
