using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Day04
{
    public class Event
    {
        private static readonly Regex EventPattern = new Regex(@"\[([^\]]{16})\] (.+)");

        public DateTime Time { get; }
        public string EventDescription { get; }

        public Event(DateTime time, string @event)
        {
            Time = time;
            EventDescription = @event;
        }

        public static Event FromString(string subject)
        {
            var match = EventPattern.Match(subject);
            return new Event(
                DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                match.Groups[2].Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Event) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Time.GetHashCode() * 397) ^ (StringComparer.InvariantCultureIgnoreCase.GetHashCode(EventDescription));
            }
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1}",
                Time.ToString("yyyy-MM-dd HH:mm"),
                EventDescription);
        }

        protected bool Equals(Event other)
        {
            return Time.Equals(other.Time) && string.Equals(EventDescription, other.EventDescription, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
