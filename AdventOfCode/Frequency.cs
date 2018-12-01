using System;
using System.Diagnostics.Contracts;

namespace AdventOfCode
{
    public class Frequency
    {
        private readonly int _frequency;

        public Frequency(int frequency)
        {
            _frequency = frequency;
        }

        public static Frequency FromString(string subject)
        {
            if (int.TryParse(subject, out var parsedInt))
            {
                return new Frequency(parsedInt);
            }

            throw new ArgumentException(string.Format("Failed to parse integer from {0}", subject));
        }

        public static Frequency operator +(Frequency a, Frequency b)
        {
            Contract.Assert(a != null, $"{nameof(a)} cannot be null");
            Contract.Assert(b != null, $"{nameof(b)} cannot be null");

            var result = new Frequency(a._frequency + b._frequency);

            return result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Frequency objFrequency))
            {
                return false;
            }

            return objFrequency._frequency == _frequency;
        }

        public override int GetHashCode()
        {
            return _frequency.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(_frequency)}: {_frequency}";
        }
    }
}
