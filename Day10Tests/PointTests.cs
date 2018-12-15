using System.Collections.Generic;
using Day10;
using Xunit;

namespace Day10Tests
{
    public class PointTests
    {
        [Theory]
        [MemberData(nameof(FromStringCases))]
        public void FromStringReturnsCorrectly(string description, int expectedX, int expectedY)
        {
            var subject = Point.FromString(description);
            Assert.Equal(expectedX, subject.PositionX);
            Assert.Equal(expectedY, subject.PositionY);
        }

        [Fact]
        public void TickTranslatesCoordinatesCorrectly()
        {
            var subject = new Point(1, 2, 3, 4);
            var expectedX = 4;
            var expectedY = 6;
            subject.Tick();
            Assert.Equal(expectedX, subject.PositionX);
            Assert.Equal(expectedY, subject.PositionY);
        }

        public static IEnumerable<object[]> FromStringCases()
        {
            yield return new object[] {"position=< 9,  1> velocity=< 0,  2>", 9, 1};
            yield return new object[] {"position=< 3, -2> velocity=<-1,  1>", 3, -2};
        }
    }
}
