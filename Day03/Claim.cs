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
            private set;
        }

        public Coordinates TopLeft
        {
            get;
            private set;
        }

        public Coordinates TopRight
        {
            get;
            private set;
        }

        public Coordinates BottomLeft
        {
            get;
            private set;
        }

        public Coordinates BottomRight
        {
            get;
            private set;
        }

        public Claim(int id, Coordinates topLeft, Coordinates topRight, Coordinates bottomLeft, Coordinates bottomRight)
        {
            Id = id;
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }

        public static Claim FromString(string claim)
        {
            var match = ClaimPattern.Match(claim);
            var id = int.Parse(match.Groups[1].Value);
            var topLeftX = int.Parse(match.Groups[2].Value);
            var topLeftY = int.Parse(match.Groups[3].Value);
            var width = int.Parse(match.Groups[4].Value);
            var height = int.Parse(match.Groups[5].Value);

            return new Claim(
                id,
                new Coordinates(topLeftX, topLeftY),
                new Coordinates(topLeftX + width, topLeftY),
                new Coordinates(topLeftX, topLeftY + height),
                new Coordinates(topLeftX + width, topLeftY + height)
            );
        }

        protected bool Equals(Claim other)
        {
            return Id == other.Id && TopLeft.Equals(other.TopLeft) && TopRight.Equals(other.TopRight) && BottomLeft.Equals(other.BottomLeft) && BottomRight.Equals(other.BottomRight);
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
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ TopLeft.GetHashCode();
                hashCode = (hashCode * 397) ^ TopRight.GetHashCode();
                hashCode = (hashCode * 397) ^ BottomLeft.GetHashCode();
                hashCode = (hashCode * 397) ^ BottomRight.GetHashCode();
                return hashCode;
            }
        }
    }
}
