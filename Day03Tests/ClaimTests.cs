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
            var claim = new Claim(
                123,
                new Coordinates(3, 2),
                new Coordinates(8, 2),
                new Coordinates(3, 6),
                new Coordinates(8, 6));
             yield return new object[] {"#123 @ 3,2: 5x4", claim};
        }
    }
}
