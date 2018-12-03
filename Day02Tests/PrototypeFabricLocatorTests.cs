using System.Collections.Generic;
using Day02;
using Xunit;

namespace Day02Tests
{
    public class PrototypeFabricLocatorTests
    {
        [Theory]
        [MemberData(nameof(CalculatorCases))]
        public void CalculateChecksumCorrectlyCalculatesChecksum(IEnumerable<BoxId> boxIds, int expectedChecksum)
        {
            var subject = new PrototypeFabricLocator();
            var actual = subject.CalculateChecksum(boxIds);
            Assert.Equal(expectedChecksum, actual);
        }

        [Theory]
        [MemberData(nameof(FinderCases))]
        public void FindSimilarBoxesCorrectlyIdentifiesSimilarPairings(IList<BoxId> boxIds,
            IEnumerable<string> expected)
        {
            var subject = new PrototypeFabricLocator();
            var actual = subject.FindSimilarBoxes(boxIds);
            Assert.Equal(expected, actual);
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

        public static IEnumerable<object[]> FinderCases()
        {
            var boxIds = new List<BoxId>()
            {
                new BoxId("abcde"),
                new BoxId("fghij"),
                new BoxId("klmno"),
                new BoxId("pqrst"),
                new BoxId("fguij"),
                new BoxId("axcye"),
                new BoxId("wvxyz"),
            };

            var expectedPairings = new List<string>()
            {
                "fgij",
            };
            yield return new object[] {boxIds, expectedPairings};
        }
    }
}
