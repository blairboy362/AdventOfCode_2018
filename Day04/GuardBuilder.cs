using System;
using System.Collections.Generic;

namespace Day04
{
    public class GuardBuilder
    {
        private int _id;
        private DateTime _wakesUpAt;
        private DateTime _fallsAsleepAt;
        private HashSet<DateTime> _sleepingMinutes;

        public GuardBuilder()
        {
            _id = default(int);
            _wakesUpAt = default(DateTime);
            _fallsAsleepAt = default(DateTime);
            _sleepingMinutes = new HashSet<DateTime>();
        }

        public GuardBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public GuardBuilder WithFallsAsleepAt(DateTime fallsAsleepAt)
        {
            if (_fallsAsleepAt != default(DateTime))
            {
                throw new InvalidOperationException("Falls Asleep At already set!");
            }

            _fallsAsleepAt = fallsAsleepAt;
            return this;
        }

        public GuardBuilder WithWakesUpAt(DateTime wakesUpAt)
        {
            if (_wakesUpAt != default(DateTime))
            {
                throw new InvalidOperationException("Wakes Up At already set!");
            }

            if (_fallsAsleepAt == default(DateTime))
            {
                throw new InvalidOperationException("Guard hasn't fallen asleep yet!");
            }

            if (wakesUpAt < _fallsAsleepAt)
            {
                throw new InvalidOperationException("Waking up before going to sleep!");
            }

            _wakesUpAt = wakesUpAt;
            var currentMinute = _fallsAsleepAt;
            while (currentMinute < _wakesUpAt)
            {
                _sleepingMinutes.Add(currentMinute);
                currentMinute = currentMinute.AddMinutes(1);
            }

            _wakesUpAt = default(DateTime);
            _fallsAsleepAt = default(DateTime);

            return this;
        }

        public Guard Build()
        {
            if (_id <= 0)
            {
                throw new InvalidOperationException(
                    string.Format("ID must be greater than 0: {0}", _id));
            }

            return new Guard(_id, _sleepingMinutes);
        }
    }
}
