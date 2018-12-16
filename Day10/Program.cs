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
            var timer = 0;
            while(skyMap.Tick())
            {
                timer++;
            }

            skyMap.Paint();

            Console.WriteLine();
            Console.WriteLine("Number of seconds: {0}", timer);
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
