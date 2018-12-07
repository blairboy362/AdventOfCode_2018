using System;
using System.Collections.Generic;
using Day05;
using Xunit;

namespace Day05Tests
{
    public class PolymerTests
    {
        [Theory]
        [MemberData(nameof(ActivateCases))]
        public void ActivateReturnsCorrectly(IList<Unit> units, string expected)
        {
            var subject = new Polymer(units);
            var actual = subject.Activate();
            Assert.Equal(expected, actual, StringComparer.InvariantCulture);
        }

        [Theory]
        [MemberData(nameof(ActivateShortestCases))]
        public void ActivateShortestReturnsCorrectly(IList<Unit> units, string expected)
        {
            var subject = new Polymer(units);
            var actual = subject.ActivateShortest();
            Assert.Equal(expected, actual, StringComparer.InvariantCulture);
        }

        public static IEnumerable<object[]> ActivateCases()
        {
            var units = new List<Unit>()
            {
                new Unit('a'),
                new Unit('A'),
            };
            yield return new object[] {units, ""};

            units = new List<Unit>()
            {
                new Unit('a'),
                new Unit('b'),
                new Unit('B'),
                new Unit('A'),
            };
            yield return new object[] {units, ""};

            units = new List<Unit>()
            {
                new Unit('a'),
                new Unit('b'),
                new Unit('A'),
                new Unit('B'),
            };
            yield return new object[] {units, "abAB"};

            units = new List<Unit>()
            {
                new Unit('a'),
                new Unit('a'),
                new Unit('b'),
                new Unit('A'),
                new Unit('A'),
                new Unit('B'),
            };
            yield return new object[] {units, "aabAAB"};

            units = new List<Unit>()
            {
                new Unit('d'),
                new Unit('a'),
                new Unit('b'),
                new Unit('A'),
                new Unit('c'),
                new Unit('C'),
                new Unit('a'),
                new Unit('C'),
                new Unit('B'),
                new Unit('A'),
                new Unit('c'),
                new Unit('C'),
                new Unit('c'),
                new Unit('a'),
                new Unit('D'),
                new Unit('A'),
            };
            yield return new object[] {units, "dabCBAcaDA"};
        }

        public static IEnumerable<object[]> ActivateShortestCases()
        {
            var units = new List<Unit>()
            {
                new Unit('d'),
                new Unit('a'),
                new Unit('b'),
                new Unit('A'),
                new Unit('c'),
                new Unit('C'),
                new Unit('a'),
                new Unit('C'),
                new Unit('B'),
                new Unit('A'),
                new Unit('c'),
                new Unit('C'),
                new Unit('c'),
                new Unit('a'),
                new Unit('D'),
                new Unit('A'),
            };
            yield return new object[] {units, "daDA"};
        }
    }
}
