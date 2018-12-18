using System;
using System.Collections.Generic;
using Utils;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing state and notes argument");
            }

            var simulator = LoadFromFile(args[0]);
            var generationCount = 50000000000;
            simulator.Simulate(generationCount);

            Console.WriteLine(
                "Sum of plant pot entries after {0} generations: {1}",
                generationCount,
                simulator.SumPottedPlants);
        }

        private static Simulator LoadFromFile(string path)
        {
            var initialState = string.Empty;
            var notes = new List<string>();

            FileHandling.Load(path, (line) =>
            {
                if (line.StartsWith("initial state: "))
                {
                    initialState = line.Trim().Split(" ")[2];
                }
                else if (line.Trim().Length > 0)
                {
                    notes.Add(line);
                }
            });

            return Simulator.FromStrings(initialState, notes);
        }
    }
}
