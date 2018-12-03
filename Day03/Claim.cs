using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day03
{
    public struct Coordinates
    {
        public readonly int X;
        public readonly int Y;

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinates other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Coordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    public class Claim
    {
        private static readonly Regex ClaimPattern = new Regex(@"#([0-9]+) @ ([0-9]+),([0-9]+): ([0-9]+)x([0-9]+)");

        public int Id
        {
            get;
        }

        public IEnumerable<Coordinates> OccupyingCoordinates
        {
            get;
        }

        public Claim(int id, IEnumerable<Coordinates> occupyingCoordinates)
        {
            Id = id;
            OccupyingCoordinates = occupyingCoordinates;
        }

        public static Claim FromString(string claim)
        {
            var match = ClaimPattern.Match(claim);
            var id = int.Parse(match.Groups[1].Value);
            var topLeftX = int.Parse(match.Groups[2].Value);
            var topLeftY = int.Parse(match.Groups[3].Value);
            var width = int.Parse(match.Groups[4].Value);
            var height = int.Parse(match.Groups[5].Value);

            var occupyingCoordinates = new List<Coordinates>();
            for (var x = topLeftX; x < topLeftX + width; x++)
            {
                for (var y = topLeftY; y < topLeftY + height; y++)
                {
                    occupyingCoordinates.Add(new Coordinates(x, y));
                }
            }

            return new Claim(id, occupyingCoordinates);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Claim) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}";
        }

        protected bool Equals(Claim other)
        {
            return Id == other.Id;
        }
    }
}
