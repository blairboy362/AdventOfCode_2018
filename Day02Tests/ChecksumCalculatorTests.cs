using System.Collections.Generic;
using Day02;
using Xunit;
using Xunit.Sdk;

namespace Day02Tests
{
    public class ChecksumCalculatorTests
    {
        [Theory]
        [MemberData(nameof(CalculatorCases))]
        public void CalculateChecksumCorrectlyCalculatesChecksum(IEnumerable<BoxId> boxIds, int expectedChecksum)
        {
            var subject = new ChecksumCalculator();
            var actual = subject.CalculateChecksum(boxIds);
            Assert.Equal(expectedChecksum, actual);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<object[]> CalculatorCases()
        {
            var boxIds = new List<BoxId>()
            {
                new BoxId("abcdef"),
                new BoxId("bababc"),
                new BoxId("abbcde"),
                new BoxId("abcccd"),
                new BoxId("aabcdd"),
                new BoxId("abcdee"),
                new BoxId("ababab"),
            };
            yield return new object[] {boxIds, 12};
        }
    }
}
