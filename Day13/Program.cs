using System;
using System.Collections.Generic;
using Utils;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing map argument");
            }

            var source = LoadFromFile(args[0]);
            var cartTrack = CartTrack.FromStrings(source);
            var coordinatesOfFirstCrash = cartTrack.FindCoordinatesOfFirstCrash();

            Console.WriteLine("Coordinates of first crash: {0}", coordinatesOfFirstCrash);

            cartTrack = CartTrack.FromStrings(source);
            var coordinatesOfRemainingCart = cartTrack.FindCoordinatesOfRemainingCart();

            Console.WriteLine("Coordinates of remaining cart: {0}", coordinatesOfRemainingCart);
        }

        private static IEnumerable<string> LoadFromFile(string path)
        {
            var source = new List<string>();

            FileHandling.Load(path, (line) =>
            {
                source.Add(line.TrimEnd());
            });

            return source;
        }
    }
}
