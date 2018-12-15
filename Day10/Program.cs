using System;
using System.Collections.Generic;
using Utils;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing locations argument");
            }

            var points = LoadFromFile(args[0]);
            var skyMap = new SkyMap(points);
            while(skyMap.Tick())
            {
            }

            skyMap.Paint();

            Console.SetCursorPosition(0, Console.BufferHeight - 1);
        }

        private static IEnumerable<Point> LoadFromFile(string path)
        {
            var points = new List<Point>();

            FileHandling.Load(path, l =>
            {
                if (!string.IsNullOrEmpty(l))
                {
                    var point = Point.FromString(l);
                    points.Add(point);
                }
            });

            return points;
        }
    }
}
