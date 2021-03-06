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

        public int SizeOfSafeRegion(int tolerance)
        {
            var map = BuildAndPopulateSafeMap();
            return map.Count(p => p.ManhattanDistances.Sum() < tolerance);
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
                        .AsParallel()
                        .Select(l => l.ManhattanDistanceFrom(coordinates))
                        .Min();
                    var closestLocations = _locations
                        .AsParallel()
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
                .AsParallel()
                .Where(p =>
                    p.Coordinates.X == _topLeft.X ||
                    p.Coordinates.X == _bottomRight.X ||
                    p.Coordinates.Y == _topLeft.Y ||
                    p.Coordinates.Y == _bottomRight.Y)
                .ToHashSet();
            var associatedLocations = pointsAtEdges
                .AsParallel()
                .Where(p => p.HasNearestLocation)
                .Select(p => p.NearestLocation)
                .ToHashSet();
            return map
                .AsParallel()
                .Where(p => p.HasNearestLocation)
                .Where(p => !(associatedLocations.Contains(p.NearestLocation)))
                .ToHashSet();
        }

        private IEnumerable<SafePoint> BuildAndPopulateSafeMap()
        {
            var map = new HashSet<SafePoint>();
            for (var x = 0; x <= _bottomRight.X; x++)
            {
                for (var y = 0; y <= _bottomRight.Y; y++)
                {
                    var coordinates = new Coordinates(x, y);
                    map.Add(new SafePoint(coordinates, _locations));
                }
            }

            return map;
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

        private class SafePoint
        {
            private readonly Coordinates _coordinates;
            public IEnumerable<int> ManhattanDistances { get; }

            public SafePoint(Coordinates coordinates, IEnumerable<Location> locations)
            {
                _coordinates = coordinates;
                ManhattanDistances = locations.Select(l => l.ManhattanDistanceFrom(_coordinates));
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((SafePoint) obj);
            }

            public override int GetHashCode()
            {
                return _coordinates.GetHashCode();
            }

            protected bool Equals(SafePoint other)
            {
                return _coordinates.Equals(other._coordinates);
            }
        }
    }
}
