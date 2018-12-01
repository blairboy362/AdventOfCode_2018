using System.Collections.Generic;
using AdventOfCode;
using Xunit;

namespace AdventOfCodeTests
{
    public class CalibratorTests
    {
        [Theory]
        [MemberData(nameof(CalibratorCases))]
        public void CalibratorReturnsCorrectResultingFrequency(
            IEnumerable<Frequency> calibratingFrequencies,
            Frequency expected)
        {
            var subject = new Calibrator();
            var actual = subject.Calibrate(calibratingFrequencies);
            Assert.Equal(expected, actual);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<object[]> CalibratorCases()
        {
            var calibratingFrequencies = new List<Frequency>
            {
                new Frequency(1),
                new Frequency(-1),
            };
            yield return new object[] {calibratingFrequencies, new Frequency(0)};

            calibratingFrequencies = new List<Frequency>
            {
                new Frequency(3),
                new Frequency(3),
                new Frequency(4),
                new Frequency(-2),
                new Frequency(-4)
            };
            yield return new object[] {calibratingFrequencies, new Frequency(10)};

            calibratingFrequencies = new List<Frequency>()
            {
                new Frequency(-6),
                new Frequency(3),
                new Frequency(8),
                new Frequency(5),
                new Frequency(-6)
            };
            yield return new object[] {calibratingFrequencies, new Frequency(5)};

            calibratingFrequencies = new List<Frequency>()
            {
                new Frequency(7),
                new Frequency(7),
                new Frequency(-2),
                new Frequency(-7),
                new Frequency(-4)
            };
            yield return new object[] {calibratingFrequencies, new Frequency(14)};
        }
    }
}
