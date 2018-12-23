using System.Collections.Generic;
using Day13;
using Xunit;

namespace Day13Tests
{
    public class CartTests
    {
        [Theory]
        [MemberData(nameof(TickCases))]
        public void TickWorksCorrectly(
            Coordinates initialCoordinates,
            CartOrientation initialOrientation,
            char mapSegment,
            Coordinates expectedCoordinates,
            CartOrientation expectedOrientation)
        {
            var subject = new Cart(initialOrientation, initialCoordinates);
            subject.Tick(mapSegment);
            Assert.Equal(expectedCoordinates, subject.Coordinates);
            Assert.Equal(expectedOrientation, subject.Orientation);
        }

        [Fact]
        public void TickTracksIntersectionDirectionCorrectly()
        {
            var subject = new Cart(CartOrientation.Right, new Coordinates(5, 5));
            subject.Tick('+');
            Assert.Equal(CartOrientation.Up, subject.Orientation);
            Assert.Equal(new Coordinates(5, 4), subject.Coordinates);
            subject.Tick('+');
            Assert.Equal(CartOrientation.Up, subject.Orientation);
            Assert.Equal(new Coordinates(5, 3), subject.Coordinates);
            subject.Tick('+');
            Assert.Equal(CartOrientation.Right, subject.Orientation);
            Assert.Equal(new Coordinates(6, 3), subject.Coordinates);
            subject.Tick('+');
            Assert.Equal(CartOrientation.Up, subject.Orientation);
            Assert.Equal(new Coordinates(6, 2), subject.Coordinates);
        }

        public static IEnumerable<object[]> TickCases()
        {
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Right, '-', new Coordinates(6, 5), CartOrientation.Right};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Left, '-', new Coordinates(4, 5), CartOrientation.Left};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Up, '|', new Coordinates(5, 4), CartOrientation.Up};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Down, '|', new Coordinates(5, 6), CartOrientation.Down};

            yield return new object[] {new Coordinates(5, 5), CartOrientation.Right, '/', new Coordinates(5, 4), CartOrientation.Up};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Left, '/', new Coordinates(5, 6), CartOrientation.Down};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Up, '/', new Coordinates(6, 5), CartOrientation.Right};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Down, '/', new Coordinates(4, 5), CartOrientation.Left};

            yield return new object[] {new Coordinates(5, 5), CartOrientation.Right, '\\', new Coordinates(5, 6), CartOrientation.Down};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Left, '\\', new Coordinates(5, 4), CartOrientation.Up};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Up, '\\', new Coordinates(4, 5), CartOrientation.Left};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Down, '\\', new Coordinates(6, 5), CartOrientation.Right};

            yield return new object[] {new Coordinates(5, 5), CartOrientation.Right, '+', new Coordinates(5, 4), CartOrientation.Up};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Left, '+', new Coordinates(5, 6), CartOrientation.Down};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Up, '+', new Coordinates(4, 5), CartOrientation.Left};
            yield return new object[] {new Coordinates(5, 5), CartOrientation.Down, '+', new Coordinates(6, 5), CartOrientation.Right};
        }
    }
}
