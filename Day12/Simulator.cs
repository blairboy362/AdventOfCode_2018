using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    public class Simulator
    {
        private readonly IDictionary<long, PlantPot> _initialState;
        private readonly IEnumerable<Note> _notes;
        private IDictionary<long, PlantPot> _forecastState;

        public Simulator(IDictionary<long, PlantPot> initialState, IEnumerable<Note> notes)
        {
            _initialState = initialState;
            _notes = notes;
            _forecastState = new Dictionary<long, PlantPot>();
        }

        public long SumPottedPlants { get; private set; }

        public static Simulator FromStrings(string initialStateDescription, IEnumerable<string> noteDescriptions)
        {
            var initialState = new Dictionary<long, PlantPot>();

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

        public void Simulate(long generations)
        {
            const int rollingBufferCapacity = 3;
            var rollingSumDifferences = new RollingBuffer<long>(rollingBufferCapacity);
            _forecastState = new Dictionary<long, PlantPot>(_initialState);
            NormalizeGeneration(_forecastState);
            PrintGeneration(_forecastState);
            SumPottedPlants = CalculatePottedPlantsSum();
            var lastGenerationSum = SumPottedPlants;

            for (var i = 0; i < generations; i++)
            {
                _forecastState = ForecastNextGeneration(_forecastState);
                NormalizeGeneration(_forecastState);
                var sumThisGeneration = CalculatePottedPlantsSum();
                rollingSumDifferences.Add(sumThisGeneration - lastGenerationSum);
                PrintGeneration(_forecastState);
                var recurrentDifference = rollingSumDifferences.First();

                if (rollingSumDifferences.All(d => d == recurrentDifference))
                {
                    var root = sumThisGeneration - (rollingBufferCapacity * recurrentDifference);
                    lastGenerationSum = root + (recurrentDifference * (generations - i + rollingBufferCapacity - 1));
                    break;
                }

                lastGenerationSum = sumThisGeneration;
            }

            SumPottedPlants = lastGenerationSum;
        }

        private static void PrintGeneration(IDictionary<long, PlantPot> generation)
        {
            foreach (var pot in generation.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value))
            {
                Console.Write(pot);
            }

            Console.WriteLine();
        }

        private static void NormalizeGeneration(IDictionary<long, PlantPot> generation)
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

        private IDictionary<long, PlantPot> ForecastNextGeneration(IDictionary<long, PlantPot> currentGeneration)
        {
            IDictionary<long, PlantPot> nextGeneration = new Dictionary<long, PlantPot>();
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

        private long CalculatePottedPlantsSum()
        {
            return _forecastState
                .Where(kvp => kvp.Value.HasPlant)
                .Sum(kvp => kvp.Key);
        }
    }
}
