using System.Collections.Generic;
using Day11;
using Xunit;

namespace Day11Tests
{
    public class FuelCellGridTests
    {
        [Theory]
        [MemberData(nameof(LargestPowerWithSizeCases))]
        public void LargestPowerOfSizeReturnsCorrectly(int gridSerialNumber, int size, Coordinates expectedTopLeft, int expectedPower)
        {
            var subject = new FuelCellGrid(gridSerialNumber).GetSquareWithLargestPower(size);
            Assert.Equal(expectedTopLeft, subject.TopLeft);
            Assert.Equal(expectedPower, subject.Power);
            Assert.Equal(size, subject.Size);
        }

        [Theory]
        [MemberData(nameof(LargestPowerCases))]
        public void LargestPowerReturnsCorrectly(int gridSerialNumber, Coordinates expectedTopLeft, int expectedPower, int expectedSize)
        {
            var subject = new FuelCellGrid(gridSerialNumber).GetSquareWithLargestPower();
            Assert.Equal(expectedTopLeft, subject.TopLeft);
            Assert.Equal(expectedPower, subject.Power);
            Assert.Equal(expectedSize, subject.Size);
        }

        public static IEnumerable<object[]> LargestPowerWithSizeCases()
        {
            yield return new object[] {18, 3, new Coordinates(33, 45), 29};
            yield return new object[] {42, 3, new Coordinates(21, 61), 30};
        }

        public static IEnumerable<object[]> LargestPowerCases()
        {
            yield return new object[] {18, new Coordinates(90, 269), 113, 16};
            yield return new object[] {42, new Coordinates(232, 251), 119, 12};
        }
    }
}
