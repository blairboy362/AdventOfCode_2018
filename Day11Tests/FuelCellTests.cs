using System.Collections.Generic;
using Day11;
using Xunit;

namespace Day11Tests
{
    public class FuelCellTests
    {
        [Theory]
        [MemberData(nameof(PowerCases))]
        public void ConstructorCalculatesPowerCorrectly(Coordinates coordinates, int gridSerialNumber, int expected)
        {
            var subject = new FuelCell(coordinates, gridSerialNumber);
            Assert.Equal(expected, subject.Power);
        }

        public static IEnumerable<object[]> PowerCases()
        {
            yield return new object[] {new Coordinates(3, 5), 8, 4};
            yield return new object[] {new Coordinates(122, 79), 57, -5};
            yield return new object[] {new Coordinates(217, 196), 39, 0};
            yield return new object[] {new Coordinates(101, 153), 71, 4};
        }
    }
}
