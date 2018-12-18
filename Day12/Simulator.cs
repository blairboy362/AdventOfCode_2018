using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    public class Simulator
    {
        private readonly IDictionary<int, PlantPot> _initialState;
        private readonly IEnumerable<Note> _notes;
        private IDictionary<int, PlantPot> _forecastState;

        public Simulator(IDictionary<int, PlantPot> initialState, IEnumerable<Note> notes)
        {
            _initialState = initialState;
            _notes = notes;
            _forecastState = new Dictionary<int, PlantPot>();
        }

        public int SumPottedPlants
        {
            get
            {
                return _forecastState
                    .Where(kvp => kvp.Value.HasPlant)
                    .Sum(kvp => kvp.Key);
            }
        }

        public static Simulator FromStrings(string initialStateDescription, IEnumerable<string> noteDescriptions)
        {
            var initialState = new Dictionary<int, PlantPot>();

            for (var i = 0; i < initialStateDescription.Length; i++)
            {
                initialState.Add(
                    i,
                    initialStateDescription[i] == '#' ? PlantPot.PotWithPlant : PlantPot.PotWithoutPlant);
            }

            var notes = new List<Note>();
            foreach (var noteDescription in noteDescriptions)
            {
                var note = Note.FromString(noteDescription);
                if (note.WillContainPlantNextGeneration)
                {
                    notes.Add(note);
                }
            }

            return new Simulator(initialState, notes);
        }

        public void Simulate(int generations)
        {
            IDictionary<int, PlantPot> forecastState = new Dictionary<int, PlantPot>(_initialState);
            NormalizeGeneration(forecastState);
            PrintGeneration(forecastState);

            for (var i = 0; i < generations; i++)
            {
                forecastState = ForecastNextGeneration(forecastState);
                NormalizeGeneration(forecastState);
                PrintGeneration(forecastState);
            }

            _forecastState = forecastState;
        }

        private static void PrintGeneration(IDictionary<int, PlantPot> generation)
        {
            foreach (var pot in generation.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value))
            {
                Console.Write(pot);
            }

            Console.WriteLine();
        }

        private static void NormalizeGeneration(IDictionary<int, PlantPot> generation)
        {
            var firstPotNumberWithPlant = generation
                .Where(kvp => kvp.Value.HasPlant)
                .OrderBy(kvp => kvp.Key)
                .First()
                .Key;
            for (var i = firstPotNumberWithPlant - 1; i >= firstPotNumberWithPlant - 5; i--)
            {
                generation[i] = PlantPot.PotWithoutPlant;
            }

            var firstPotNumber = generation
                .OrderBy(kvp => kvp.Key)
                .First()
                .Key;
            for (var i = firstPotNumber; i < firstPotNumberWithPlant - 5; i++)
            {
                generation.Remove(i);
            }

            var lastPotNumberWithPlant = generation
                .Where(kvp => kvp.Value.HasPlant)
                .OrderByDescending(kvp => kvp.Key)
                .First()
                .Key;
            for (var i = lastPotNumberWithPlant + 1; i <= lastPotNumberWithPlant + 5; i++)
            {
                generation[i] = PlantPot.PotWithoutPlant;
            }

            var lastPotNumber = generation
                .OrderByDescending(kvp => kvp.Key)
                .First()
                .Key;
            for (var i = lastPotNumber; i > lastPotNumberWithPlant + 5; i--)
            {
                generation.Remove(i);
            }
        }

        private IDictionary<int, PlantPot> ForecastNextGeneration(IDictionary<int, PlantPot> currentGeneration)
        {
            IDictionary<int, PlantPot> nextGeneration = new Dictionary<int, PlantPot>();
            var startingPotNumber = currentGeneration.Keys.OrderBy(k => k).First();

            for (var i = 0; i < currentGeneration.Count; i++)
            {
                var potNumber = i + startingPotNumber;
                var matchingNote = _notes.SingleOrDefault(n => n.Matches(currentGeneration, potNumber));
                if (matchingNote == default(Note))
                {
                    nextGeneration.Add(potNumber, PlantPot.PotWithoutPlant);
                }
                else
                {
                    nextGeneration.Add(potNumber, matchingNote.WillContainPlantNextGeneration ? PlantPot.PotWithPlant : PlantPot.PotWithoutPlant);
                }
            }

            return nextGeneration;
        }
    }
}
