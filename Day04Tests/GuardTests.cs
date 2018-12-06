using System;
using Day04;
using Xunit;

namespace Day04Tests
{
    public class GuardTests
    {
        [Fact]
        public void SleepiestMinuteReturnsCorrectly()
        {
            var subject = new GuardBuilder()
                .WithId(1)
                .WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 5, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 1, 0, 25, 0))
                .WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 30, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 1, 0, 55, 0))
                .WithFallsAsleepAt(new DateTime(1518, 11, 3, 0, 24, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 3, 0, 29, 0))
                .Build();
            var expected = 24;
            var actual = subject.SleepiestMinute().Minute;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TotalMinutesAsleepReturnsCorrectly()
        {
            var subject = new GuardBuilder()
                .WithId(1)
                .WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 5, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 1, 0, 25, 0))
                .WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 30, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 1, 0, 55, 0))
                .WithFallsAsleepAt(new DateTime(1518, 11, 3, 0, 24, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 3, 0, 29, 0))
                .Build();
            var expected = 50;
            var actual = subject.TotalMinutesAsleep();
            Assert.Equal(expected, actual);
        }
    }
}
