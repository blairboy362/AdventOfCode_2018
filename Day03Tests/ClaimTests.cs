using System.Collections.Generic;
using Day03;
using Xunit;

namespace Day03Tests
{
    public class ClaimTests
    {
        [Theory]
        [MemberData(nameof(FromStringCases))]
        public void FromStringCorrectlyPopulatesProperties(string claim, Claim expected)
        {
            var actual = Claim.FromString(claim);
            Assert.Equal(expected, actual);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<object[]> FromStringCases()
        {
            var occupyingCoordinates = new List<Coordinates>()
            {
                new Coordinates(3, 2),
                new Coordinates(3, 3),
                new Coordinates(3, 4),
                new Coordinates(3, 5),
                new Coordinates(4, 2),
                new Coordinates(4, 3),
                new Coordinates(4, 4),
                new Coordinates(4, 5),
                new Coordinates(5, 2),
                new Coordinates(5, 3),
                new Coordinates(5, 4),
                new Coordinates(5, 5),
                new Coordinates(6, 2),
                new Coordinates(6, 3),
                new Coordinates(6, 4),
                new Coordinates(6, 5),
                new Coordinates(7, 2),
                new Coordinates(7, 3),
                new Coordinates(7, 4),
                new Coordinates(7, 5),
                new Coordinates(8, 2),
                new Coordinates(8, 3),
                new Coordinates(8, 4),
                new Coordinates(8, 5),
            };
            var claim = new Claim(123, occupyingCoordinates);
             yield return new object[] {"#123 @ 3,2: 5x4", claim};
        }
    }
}
