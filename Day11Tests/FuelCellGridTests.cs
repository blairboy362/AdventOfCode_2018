using System.Collections.Generic;
using Day11;
using Xunit;

namespace Day11Tests
{
    public class FuelCellGridTests
    {
        [Theory]
        [MemberData(nameof(LargestPowerCases))]
        public void LargestPowerReturnsCorrectly(int gridSerialNumber, Coordinates expectedTopLeft, int expectedPower)
        {
            var subject = new FuelCellGrid(gridSerialNumber, 300, 300).LargestPower;
            Assert.Equal(expectedTopLeft, subject.TopLeft);
            Assert.Equal(expectedPower, subject.Power);
        }

        public static IEnumerable<object[]> LargestPowerCases()
        {
            yield return new object[] {18, new Coordinates(33, 45), 29};
            yield return new object[] {42, new Coordinates(21, 61), 30};
        }
    }
}
