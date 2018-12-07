using System;
using System.Collections.Generic;
using Day04;
using Xunit;

namespace Day04Tests
{
    public class ShiftAnalyserTests
    {
        [Theory]
        [MemberData(nameof(FindGuardAsleepTheMostCases))]
        public void FindGuardAsleepTheMostReturnsCorrectly(IEnumerable<Event> events, Guard expected)
        {
            var subject = new ShiftAnalyser(events);
            var actual = subject.FindGuardAsleepTheMost();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(FindGuardAsleepTheSameMinuteCases))]
        public void FindGuardAsleepTheSameMinuteTheMostReturnsCorrectly(IEnumerable<Event> events, Guard expected)
        {
            var subject = new ShiftAnalyser(events);
            var actual = subject.FindGuardAsleepTheSameMinuteTheMost();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> FindGuardAsleepTheMostCases()
        {
            var events = new List<Event>()
            {
                Event.FromString("[1518-11-01 00:00] Guard #10 begins shift"),
                Event.FromString("[1518-11-01 00:05] falls asleep"),
                Event.FromString("[1518-11-01 00:25] wakes up"),
                Event.FromString("[1518-11-01 00:30] falls asleep"),
                Event.FromString("[1518-11-01 00:55] wakes up"),
                Event.FromString("[1518-11-01 23:58] Guard #99 begins shift"),
                Event.FromString("[1518-11-02 00:40] falls asleep"),
                Event.FromString("[1518-11-02 00:50] wakes up"),
                Event.FromString("[1518-11-03 00:05] Guard #10 begins shift"),
                Event.FromString("[1518-11-03 00:24] falls asleep"),
                Event.FromString("[1518-11-03 00:29] wakes up"),
                Event.FromString("[1518-11-04 00:02] Guard #99 begins shift"),
                Event.FromString("[1518-11-04 00:36] falls asleep"),
                Event.FromString("[1518-11-04 00:46] wakes up"),
                Event.FromString("[1518-11-05 00:03] Guard #99 begins shift"),
                Event.FromString("[1518-11-05 00:45] falls asleep"),
                Event.FromString("[1518-11-05 00:55] wakes up"),
            };
            var expected = new Guard(10, new HashSet<DateTime>());
            yield return new object[] {events, expected};
        }

        public static IEnumerable<object[]> FindGuardAsleepTheSameMinuteCases()
        {
            var events = new List<Event>()
            {
                Event.FromString("[1518-11-01 00:00] Guard #10 begins shift"),
                Event.FromString("[1518-11-01 00:05] falls asleep"),
                Event.FromString("[1518-11-01 00:25] wakes up"),
                Event.FromString("[1518-11-01 00:30] falls asleep"),
                Event.FromString("[1518-11-01 00:55] wakes up"),
                Event.FromString("[1518-11-01 23:58] Guard #99 begins shift"),
                Event.FromString("[1518-11-02 00:40] falls asleep"),
                Event.FromString("[1518-11-02 00:50] wakes up"),
                Event.FromString("[1518-11-03 00:05] Guard #10 begins shift"),
                Event.FromString("[1518-11-03 00:24] falls asleep"),
                Event.FromString("[1518-11-03 00:29] wakes up"),
                Event.FromString("[1518-11-04 00:02] Guard #99 begins shift"),
                Event.FromString("[1518-11-04 00:36] falls asleep"),
                Event.FromString("[1518-11-04 00:46] wakes up"),
                Event.FromString("[1518-11-05 00:03] Guard #99 begins shift"),
                Event.FromString("[1518-11-05 00:45] falls asleep"),
                Event.FromString("[1518-11-05 00:55] wakes up"),
            };
            var expected = new Guard(99, new HashSet<DateTime>());
            yield return new object[] {events, expected};
        }
    }
}
