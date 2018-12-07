using System;
using System.Collections.Generic;
using Utils;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing events argument");
            }

            var events = LoadFromFile(args[0]);
            var shiftAnalyser = new ShiftAnalyser(events);
            var sleepyGuard = shiftAnalyser.FindGuardAsleepTheMost();
            var sleepiestMinute = sleepyGuard.SleepiestMinute().Minute;

            Console.WriteLine(
                "Guard asleep the most has ID: {0}, most frequent minute asleep {1} giving an answer of {2}.",
                sleepyGuard.Id,
                sleepiestMinute,
                sleepyGuard.Id * sleepiestMinute);

            var consistentSleeper = shiftAnalyser.FindGuardAsleepTheSameMinuteTheMost();
            var favouredMinute = consistentSleeper.SleepiestMinute().Minute;

            Console.WriteLine(
                "Guard asleep most consistently has ID: {0}, the minute is {1} so the answer is {2}",
                consistentSleeper.Id,
                favouredMinute,
                consistentSleeper.Id * favouredMinute);
        }

        private static IEnumerable<Event> LoadFromFile(string path)
        {
            var events = new List<Event>();

            FileHandling.Load(path, l =>
            {
                var @event = Event.FromString(l);
                events.Add(@event);
            });

            return events;
        }
    }
}
