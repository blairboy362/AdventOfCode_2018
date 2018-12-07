using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    public class Universe
    {
        private readonly Coordinates _topLeft;
        private readonly Coordinates _bottomRight;
        private readonly IEnumerable<Location> _locations;

        public Universe(IEnumerable<Location> locations)
        {
            _locations = locations;
            _topLeft = new Coordinates(0, 0);
            _bottomRight = new Coordinates(
                _locations.Max(l => l.Coordinates.X),
                _locations.Max(l => l.Coordinates.Y));
        }

        public int SizeOfLargestNonInfiniteArea()
        {
            var map = BuildAndPopulateMap();
            var finiteAreas = TrimInfiniteAreas(map);
            return finiteAreas
                .GroupBy(a => a.NearestLocation)
                .OrderByDescending(g => g.Count())
                .First()
                .Count();
        }

        private IEnumerable<Point> BuildAndPopulateMap()
        {
            var map = new HashSet<Point>();
            for (var x = 0; x <= _bottomRight.X; x++)
            {
                for (var y = 0; y <= _bottomRight.Y; y++)
                {
                    var coordinates = new Coordinates(x, y);
                    var closestDistance = _locations
                        .Select(l => l.ManhattanDistanceFrom(coordinates))
                        .Min();
                    var closestLocations = _locations
                        .Where(l => l.ManhattanDistanceFrom(coordinates) == closestDistance);

                    if (closestLocations.Count() == 1)
                    {
                        map.Add(new Point(coordinates, true, closestLocations.Single()));
                    }
                    else
                    {
                        map.Add(new Point(coordinates, false, null));
                    }
                }
            }

            return map;
        }

        private IEnumerable<Point> TrimInfiniteAreas(IEnumerable<Point> map)
        {
            var pointsAtEdges = map
                .Where(p =>
                    p.Coordinates.X == _topLeft.X ||
                    p.Coordinates.X == _bottomRight.X ||
                    p.Coordinates.Y == _topLeft.Y ||
                    p.Coordinates.Y == _bottomRight.Y);
            var associatedLocations = pointsAtEdges
                .Where(p => p.HasNearestLocation)
                .Select(p => p.NearestLocation);
            return map
                .Where(p => p.HasNearestLocation)
                .Where(p => !(associatedLocations.Contains(p.NearestLocation)));
        }

        private class Point
        {
            public Coordinates Coordinates { get; }
            public bool HasNearestLocation { get; }
            public Location NearestLocation { get; }

            public Point(Coordinates coordinates, bool hasNearestLocation, Location nearestLocation)
            {
                Coordinates = coordinates;
                HasNearestLocation = hasNearestLocation;
                NearestLocation = nearestLocation;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Point) obj);
            }

            public override int GetHashCode()
            {
                return Coordinates.GetHashCode();
            }

            protected bool Equals(Point other)
            {
                return Coordinates.Equals(other.Coordinates);
            }
        }
    }
}
