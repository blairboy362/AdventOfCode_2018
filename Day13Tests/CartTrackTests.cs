using System.Collections.Generic;
using Day13;
using Xunit;

namespace Day13Tests
{
    public class CartTrackTests
    {
        [Theory]
        [MemberData(nameof(FirstCrashCases))]
        public void FindCoordinatesOfFirstCrashSucceeds(IEnumerable<string> sourceMap, Coordinates expected)
        {
            var subject = CartTrack.FromStrings(sourceMap);
            var actual = subject.FindCoordinatesOfFirstCrash();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(LastRemainingCases))]
        public void FindCoordinatesOfRemainingCartSucceeds(IEnumerable<string> sourceMap, Coordinates expected)
        {
            var subject = CartTrack.FromStrings(sourceMap);
            var actual = subject.FindCoordinatesOfRemainingCart();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> FirstCrashCases()
        {
            var sourceMap = new[]
            {
                @"/->-\        ",
                @"|   |  /----\",
                @"| /-+--+-\  |",
                @"| | |  | v  |",
                @"\-+-/  \-+--/",
                @"  \------/   ",
            };

            yield return new object[] {sourceMap, new Coordinates(7, 3)};
        }

        public static IEnumerable<object[]> LastRemainingCases()
        {
            var sourceMap = new[]
            {
                @"/>-<\  ",
                @"|   |  ",
                @"| /<+-\",
                @"| | | v",
                @"\>+</ |",
                @"  |   ^",
                @"  \<->/",
            };

            yield return new object[] {sourceMap, new Coordinates(6, 4)};
        }
    }
}
