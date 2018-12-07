using System;
using System.Collections.Generic;
using System.IO;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing polymer argument");
            }

            var suitBefore = LoadFromFile(args[0]);
            var polymer = new Polymer(suitBefore);
            var suitAfter = polymer.Activate();

            Console.WriteLine("Suit after activation has length {0}", suitAfter.Length);

            var shortestPolymer = polymer.ActivateShortest();

            Console.WriteLine("Shortest suit polymer has length: {0}", shortestPolymer.Length);
        }

        private static IList<Unit> LoadFromFile(string path)
        {
            var polymerContents = File.ReadAllText(path).Trim();
            var polymer = new List<Unit>();
            foreach (var unitType in polymerContents)
            {
                polymer.Add(new Unit(unitType));
            }

            return polymer;
        }
    }
}
