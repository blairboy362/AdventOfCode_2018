using System.Collections.Generic;
using Day12;
using Xunit;

namespace Day12Tests
{
    public class SimulatorTests
    {
        [Theory]
        [MemberData(nameof(SimulateCases))]
        public void SimulateCompletesCorrectly(IDictionary<long, PlantPot> initialState, IEnumerable<Note> notes, long generations, long expectedSum)
        {
            var subject = new Simulator(initialState, notes);
            subject.Simulate(generations);
            Assert.Equal(expectedSum, subject.SumPottedPlants);
        }

        public static IEnumerable<object[]> SimulateCases()
        {
            var initialState = new Dictionary<long, PlantPot>()
            {
                {0, PlantPot.PotWithPlant},
                {1, PlantPot.PotWithoutPlant},
                {2, PlantPot.PotWithoutPlant},
                {3, PlantPot.PotWithPlant},
                {4, PlantPot.PotWithoutPlant},
                {5, PlantPot.PotWithPlant},
                {6, PlantPot.PotWithoutPlant},
                {7, PlantPot.PotWithoutPlant},
                {8, PlantPot.PotWithPlant},
                {9, PlantPot.PotWithPlant},
                {10, PlantPot.PotWithoutPlant},
                {11, PlantPot.PotWithoutPlant},
                {12, PlantPot.PotWithoutPlant},
                {13, PlantPot.PotWithoutPlant},
                {14, PlantPot.PotWithoutPlant},
                {15, PlantPot.PotWithoutPlant},
                {16, PlantPot.PotWithPlant},
                {17, PlantPot.PotWithPlant},
                {18, PlantPot.PotWithPlant},
                {19, PlantPot.PotWithoutPlant},
                {20, PlantPot.PotWithoutPlant},
                {21, PlantPot.PotWithoutPlant},
                {22, PlantPot.PotWithPlant},
                {23, PlantPot.PotWithPlant},
                {24, PlantPot.PotWithPlant},
            };

            var notes = new List<Note>()
            {
                Note.FromString("...## => #"),
                Note.FromString("..#.. => #"),
                Note.FromString(".#... => #"),
                Note.FromString(".#.#. => #"),
                Note.FromString(".#.## => #"),
                Note.FromString(".##.. => #"),
                Note.FromString(".#### => #"),
                Note.FromString("#.#.# => #"),
                Note.FromString("#.### => #"),
                Note.FromString("##.#. => #"),
                Note.FromString("##.## => #"),
                Note.FromString("###.. => #"),
                Note.FromString("###.# => #"),
                Note.FromString("####. => #"),
            };

            yield return new object[] {initialState, notes, 20, 325};
        }
    }
}
