using System;
using System.Collections.Generic;
using System.Linq;

namespace Day07
{
    public class Step : IComparable<Step>
    {
        private readonly char _id;
        private readonly ICollection<Step> _prerequisites;

        public Step(char id)
        {
            _id = id;
            _prerequisites = new HashSet<Step>();
        }

        public bool Completed
        {
            get;
            private set;
        }

        public bool CanStart()
        {
            return !Completed && (_prerequisites.Count == 0 || _prerequisites.All(p => p.Completed));
        }

        public char Complete()
        {
            if (!CanStart())
            {
                throw new InvalidOperationException("Cannot complete - incomplete prerequisites exist!");
            }

            Completed = true;
            return _id;
        }

        public void AddPrerequisite(Step prerequisite)
        {
            if (Completed)
            {
                throw new InvalidOperationException("Cannot add a prerequisite to a completed step!");
            }

            _prerequisites.Add(prerequisite);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Step) obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_id}";
        }

        public int CompareTo(Step other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _id.CompareTo(other._id);
        }

        protected bool Equals(Step other)
        {
            return _id == other._id;
        }
    }
}
