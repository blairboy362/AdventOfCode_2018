using System;
using System.Collections.Generic;
using System.Linq;

namespace Day04
{
    public struct SleepingMinuteDataPoint
    {
        public readonly int Minute;
        public readonly int Frequency;

        public SleepingMinuteDataPoint(int minute, int frequency)
        {
            Minute = minute;
            Frequency = frequency;
        }

        public bool Equals(SleepingMinuteDataPoint other)
        {
            return Minute == other.Minute && Frequency == other.Frequency;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SleepingMinuteDataPoint other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Minute * 397) ^ Frequency;
            }
        }
    }

    public class Guard
    {
        public int Id { get; }

        private readonly IEnumerable<DateTime> _sleepingMinutes;
        private readonly IEnumerable<SleepingMinuteDataPoint> _sleepingMinutesHistogram;

        public Guard(int id, IEnumerable<DateTime> sleepingMinutes)
        {
            Id = id;
            _sleepingMinutes = sleepingMinutes;
            _sleepingMinutesHistogram = _sleepingMinutes
                .GroupBy(m => m.Minute)
                .Select(g => new SleepingMinuteDataPoint(g.Key, g.Count()));
        }

        public Guard(Guard baseRecord, Guard newRecord)
            :this(baseRecord.Id, baseRecord._sleepingMinutes.Concat(newRecord._sleepingMinutes))
        {
        }

        public int TotalMinutesAsleep()
        {
            return _sleepingMinutesHistogram.Sum(m => m.Frequency);
        }

        public SleepingMinuteDataPoint SleepiestMinute()
        {
            return _sleepingMinutesHistogram
                .OrderByDescending(m => m.Frequency)
                .First();
        }

        protected bool Equals(Guard other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Guard) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
