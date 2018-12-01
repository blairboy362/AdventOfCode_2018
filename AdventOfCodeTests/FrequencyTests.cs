using System;
using AdventOfCode;
using Xunit;

namespace AdventOfCodeTests
{
    public class FrequencyTests
    {
        [Theory]
        [InlineData(2, "2")]
        [InlineData(3, "+3")]
        [InlineData(-4, "-4")]
        public void FromStringCreatesCorrectFrequency(int expectedFrequency, string subject)
        {
            var expected = new Frequency(expectedFrequency);
            var actual = Frequency.FromString(subject);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromStringThrowsOnGarbage()
        {
            Assert.Throws<ArgumentException>(() => Frequency.FromString("not a number"));
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-1, 2, 1)]
        [InlineData(-1, -1, -2)]
        public void AdditionOperatorBehavesCorrectly(int a, int b, int expectedResult)
        {
            var expected = new Frequency(expectedResult);
            var frequencyA = new Frequency(a);
            var frequencyB = new Frequency(b);
            var actual = frequencyA + frequencyB;

            Assert.Equal(expected, actual);
        }
    }
}
