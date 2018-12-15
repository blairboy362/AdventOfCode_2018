using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day10
{
    public class SkyMap
    {
        private readonly IEnumerable<Point> _points;

        private int _previousSizeX;
        private int _previousSizeY;

        public int SizeX
        {
            get
            {
                return _points.Max(p => p.PositionX) - _points.Min(p => p.PositionX);
            }
        }

        public int SizeY
        {
            get
            {
                return _points.Max(p => p.PositionY) - _points.Min(p => p.PositionY);
            }
        }

        public SkyMap(IEnumerable<Point> points)
        {
            _points = points;
            _previousSizeX = SizeX;
            _previousSizeY = SizeY;
        }

        public bool Tick(int count = 1)
        {
            Parallel.ForEach(_points, (point) => { point.Tick(count); });
            var newSizeX = SizeX;
            var newSizeY = SizeY;
            if (newSizeX < _previousSizeX && newSizeY < _previousSizeY)
            {
                _previousSizeX = newSizeX;
                _previousSizeY = newSizeY;
                return true;
            }

            Parallel.ForEach(_points, (point) => { point.TickBack(); });

            return false;
        }

        public void Paint()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            var minX = _points.Min(p => p.PositionX);
            var minY = _points.Min(p => p.PositionY);
            for (var y = minY; y <= minY + _previousSizeY; y++)
            {
                for (var x = minX; x <= minX + _previousSizeX; x++)
                {
                    if (_points.Any(p => p.PositionX == x && p.PositionY == y))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
