using System;
using System.Collections.Generic;
using Utils;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing locations argument");
            }

            var locations = LoadFromFile(args[0]);
            var universe = new Universe(locations);
            //var largestAreaSize = universe.SizeOfLargestNonInfiniteArea();
            //Console.WriteLine("The largest area size is {0}", largestAreaSize);
            var sizeOfSafeRegion = universe.SizeOfSafeRegion(10000);
            Console.WriteLine("The safe region size is {0}", sizeOfSafeRegion);
        }

        private static IEnumerable<Location> LoadFromFile(string path)
        {
            var locations = new HashSet<Location>();

            FileHandling.Load(path, l =>
            {
                var location = Location.FromString(l);
                locations.Add(location);
            });

            return locations;
        }
    }
}
