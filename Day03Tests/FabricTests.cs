using System.Collections.Generic;
using Day03;
using Xunit;

namespace Day03Tests
{
    public class FabricTests
    {
        [Theory]
        [MemberData(nameof(OverlapCases))]
        public void CountOverlappingClaimsReturnsCorrectCount(IEnumerable<Claim> claims, int expected)
        {
            var subject = new Fabric();
            var actual = subject.CountOverlappingClaims(claims);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> OverlapCases()
        {
            var claims = new List<Claim>()
            {
                Claim.FromString("#1 @ 1,3: 4x4"),
                Claim.FromString("#2 @ 3,1: 4x4"),
                Claim.FromString("#3 @ 5,5: 2x2"),
            };

            yield return new object[] {claims, 4};
        }
    }
}
