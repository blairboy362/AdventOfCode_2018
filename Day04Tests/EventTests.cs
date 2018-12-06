using System;
using System.Collections.Generic;
using Day04;
using Xunit;

namespace Day04Tests
{
    public class EventTests
    {
        [Theory]
        [MemberData(nameof(FromStringCases))]
        public void FromStringReturnsCorrectly(string @event, Event expected)
        {
            var actual = Event.FromString(@event);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> FromStringCases()
        {
            var @event = new Event(
                new DateTime(1518, 11, 01, 0, 0, 0),
                "Guard #10 begins shift");
            yield return new object[] {"[1518-11-01 00:00] Guard #10 begins shift", @event};

            @event = new Event(
                new DateTime(1518, 11, 02, 0, 40, 0), "falls asleep");
            yield return new object[] {"[1518-11-02 00:40] falls asleep", @event};
        }
    }
}
