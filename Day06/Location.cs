using System;
using System.Text.RegularExpressions;

namespace Day06
{
    public class Location
    {
        private static readonly Regex CoordinatePattern = new Regex(@"([0-9]+), *([0-9]+)");

        public Coordinates Coordinates { get; }

        public Location(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public static Location FromString(string location)
        {
            var matches = CoordinatePattern.Match(location);
            return new Location(
                new Coordinates(
                    int.Parse(matches.Groups[1].Value),
                    int.Parse(matches.Groups[2].Value)));
        }

        public int ManhattanDistanceFrom(Coordinates coordinates)
        {
            return Math.Abs(Coordinates.X - coordinates.X) + Math.Abs(Coordinates.Y - coordinates.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Location) obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Coordinates}";
        }

        protected bool Equals(Location other)
        {
            return Coordinates.Equals(other.Coordinates);
        }
    }
}
