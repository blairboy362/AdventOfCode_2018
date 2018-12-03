using Day02;
using Xunit;

namespace Day02Tests
{
    public class BoxIdTests
    {
        [Theory]
        [InlineData("abcdef", false, false)]
        [InlineData("bababc", true, true)]
        [InlineData("abbcde", true, false)]
        [InlineData("abcccd", false, true)]
        [InlineData("aabcdd", true, false)]
        [InlineData("abcdee", true, false)]
        [InlineData("ababab", false, true)]
        public void ConstructorCorrectlyInitialisesFacts(string boxId, bool expectedExactlyTwo, bool expectedExactlyThree)
        {
            var subject = new BoxId(boxId);
            Assert.Equal(subject.ContainsExactlyTwo, expectedExactlyTwo);
            Assert.Equal(subject.ContainsExactlyThree, expectedExactlyThree);
        }

        [Theory]
        [InlineData("abcde", "axcye", false)]
        [InlineData("fghij", "fguij", true)]
        public void SimilarReturnsCorrectly(string boxIdA, string boxIdB, bool expected)
        {
            var subject = new BoxId(boxIdA);
            var candidate = new BoxId(boxIdB);
            var actual = subject.Similar(candidate);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("fghij", "fguij", "fgij")]
        public void CommonCharactersReturnsCorrectly(string boxIdA, string boxIdB, string expected)
        {
            var subject = new BoxId(boxIdA);
            var candidate = new BoxId(boxIdB);
            var actual = subject.CommonCharacters(candidate);
            Assert.Equal(actual, expected);
        }
    }
}
