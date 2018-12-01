using System.Collections.Generic;
using AdventOfCode;
using Xunit;

namespace AdventOfCodeTests
{
    public class CalibratorTests
    {
        [Fact]
        public void CalibratorReturnsCorrectResultingFrequency()
        {
            var calibratingFrequencies = new List<Frequency>
            {
                new Frequency(1),
                new Frequency(2),
                new Frequency(-1)
            };
            var subject = new Calibrator();
            var expected = new Frequency(2);
            var actual = subject.Calibrate(calibratingFrequencies);
            Assert.Equal(expected, actual);
        }
    }
}