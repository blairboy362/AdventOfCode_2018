using System.Collections.Generic;
using Day06;
using Xunit;

namespace Day06Tests
{
    public class LocationTests
    {
        [Theory]
        [MemberData(nameof(FromStringCases))]
        public void FromStringReturnsCorrectly(string coordinates, Location expected)
        {
            var actual = Location.FromString(coordinates);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ManhattanDistanceCases))]
        public void ManhattanDistanceFromReturnsCorrectly(Coordinates location, Coordinates point, int expected)
        {
            var subject = new Location(location);
            var actual = subject.ManhattanDistanceFrom(point);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> FromStringCases()
        {
            yield return new object[] {"1, 1", new Location(new Coordinates(1, 1))};
            yield return new object[] {"1, 6", new Location(new Coordinates(1, 6))};
            yield return new object[] {"8, 3", new Location(new Coordinates(8, 3))};
            yield return new object[] {"3, 4", new Location(new Coordinates(3, 4))};
            yield return new object[] {"5, 5", new Location(new Coordinates(5, 5))};
            yield return new object[] {"8, 9", new Location(new Coordinates(8, 9))};
        }

        public static IEnumerable<object[]> ManhattanDistanceCases()
        {
            var location = new Coordinates(3, 3);
            yield return new object[] {location, new Coordinates(1, 1), 4};
            yield return new object[] {location, new Coordinates(1, 6), 5};
            yield return new object[] {location, new Coordinates(6, 1), 5};
            yield return new object[] {location, new Coordinates(6, 6), 6};
        }
    }
}
