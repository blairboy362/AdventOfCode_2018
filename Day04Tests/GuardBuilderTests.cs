using System;
using System.Collections.Generic;
using Day04;
using Xunit;

namespace Day04Tests
{
    public class GuardBuilderTests
    {
        [Fact]
        public void BuildWorksCorrectly()
        {
            var sleepingMinutes = new HashSet<DateTime>();
            var expected = new Guard(1, sleepingMinutes);
            var subject = new GuardBuilder()
                .WithId(1)
                .WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 5, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 1, 0, 25, 0));

            var actual = subject.Build();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuilderAcceptsMultipleSleeps()
        {
            var subject = new GuardBuilder();
            var expected = new Guard(1, new HashSet<DateTime>());
            var actual = subject
                .WithId(1)
                .WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 5, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 1, 0, 25, 0))
                .WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 30, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 1, 0, 55, 0))
                .Build();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildThrowsOnVariousScenarios()
        {
            Assert.Throws<InvalidOperationException>(() => new GuardBuilder().Build());
            Assert.Throws<InvalidOperationException>(() => new GuardBuilder().WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 5, 0)).Build());
            Assert.Throws<InvalidOperationException>(() => new GuardBuilder().WithWakesUpAt(new DateTime(1518, 11, 1, 0, 25, 0)).Build());
            Assert.Throws<InvalidOperationException>(() => new GuardBuilder()
                .WithId(1)
                .WithFallsAsleepAt(new DateTime(1518, 11, 1, 0, 25, 0))
                .WithWakesUpAt(new DateTime(1518, 11, 1, 0, 5, 0))
                .Build());
        }
    }
}
