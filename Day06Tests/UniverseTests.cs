using System.Collections.Generic;
using Day06;
using Xunit;

namespace Day06Tests
{
    public class UniverseTests
    {
        [Theory]
        [MemberData(nameof(AreaCases))]
        public void SizeOfLargestNonInfiniteAreaReturnsCorrectly(IEnumerable<Location> locations, int expected)
        {
            var subject = new Universe(locations);
            var actual = subject.SizeOfLargestNonInfiniteArea();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> AreaCases()
        {
            var locations = new HashSet<Location>()
            {
                new Location(new Coordinates(1, 1)),
                new Location(new Coordinates(1, 6)),
                new Location(new Coordinates(8, 3)),
                new Location(new Coordinates(3, 4)),
                new Location(new Coordinates(5, 5)),
                new Location(new Coordinates(8, 9)),
            };
            yield return new object[] {locations, 17};
        }
    }
}
