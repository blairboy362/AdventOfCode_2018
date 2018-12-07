using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    public class ShiftAnalyser
    {
        private static readonly Regex GuardIdPattern = new Regex(@"Guard #(\d+) begins shift");

        private readonly IEnumerable<Event> _events;
        private readonly HashSet<Guard> _guards;

        public ShiftAnalyser(IEnumerable<Event> events)
        {
            _events = events;
            _guards = new HashSet<Guard>();
            AnalyseShifts();
        }

        private IEnumerable<Guard> SleepingGuards
        {
            get { return _guards.Where(g => g.Slept()); }
        }

        public Guard FindGuardAsleepTheMost()
        {
            var longestSleep = SleepingGuards
                .Max(g => g.TotalMinutesAsleep());
            var guardsAsleepLongest = SleepingGuards
                .Where(g => g.TotalMinutesAsleep() == longestSleep);
            if (guardsAsleepLongest.Count() > 1)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "More than one guard is asleep the longest! {0}",
                        guardsAsleepLongest.Count()));
            }

            return guardsAsleepLongest.Single();
        }

        public Guard FindGuardAsleepTheSameMinuteTheMost()
        {
            var highestFrequency = SleepingGuards.Max(g => g.SleepiestMinute().Frequency);
            var candidates = SleepingGuards.Where(g => g.SleepiestMinute().Frequency == highestFrequency);
            if (candidates.Count() > 1)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "More than one guard is asleep the most often on the same minute! {0}",
                        candidates.Count()));
            }

            return candidates.Single();
        }

        private void AnalyseShifts()
        {
            var sortedEvents = _events.OrderBy(e => e.Time);
            var currentGuardBuilder = default(GuardBuilder);
            foreach (var @event in sortedEvents)
            {
                var guardIdMatch = GuardIdPattern.Match(@event.EventDescription);
                if (guardIdMatch.Success)
                {
                    AddNewGuard(currentGuardBuilder);

                    currentGuardBuilder = new GuardBuilder()
                        .WithId(int.Parse(guardIdMatch.Groups[1].Value));
                }
                else if (string.Equals(@event.EventDescription, "falls asleep", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (currentGuardBuilder == default(GuardBuilder))
                    {
                        throw new InvalidOperationException(
                            string.Format("Guard builder not set and encountered {0}", @event));
                    }

                    currentGuardBuilder = currentGuardBuilder.WithFallsAsleepAt(@event.Time);
                }
                else if (string.Equals(@event.EventDescription, "wakes up",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    if (currentGuardBuilder == default(GuardBuilder))
                    {
                        throw new InvalidOperationException(
                            string.Format("Guard builder not set and encountered {0}", @event));
                    }

                    currentGuardBuilder = currentGuardBuilder.WithWakesUpAt(@event.Time);
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format("Don't know what to do with {0}", @event));
                }
            }

            AddNewGuard(currentGuardBuilder);
        }

        private void AddNewGuard(GuardBuilder currentGuardBuilder)
        {
            if (currentGuardBuilder != default(GuardBuilder))
            {
                var newGuard = currentGuardBuilder.Build();
                if (_guards.Contains(newGuard))
                {
                    if (!_guards.TryGetValue(newGuard, out var existingGuard))
                    {
                        throw new InvalidOperationException(
                            string.Format("Guard contained but not gettable: {0}", newGuard));
                    }

                    if (!_guards.Remove(existingGuard))
                    {
                        throw new InvalidOperationException(
                            string.Format("Failed to remove existing guard: {0}", existingGuard));
                    }

                    newGuard = new Guard(existingGuard, newGuard);
                }

                _guards.Add(newGuard);
            }
        }
    }
}
