using System.Collections.Generic;
using Day12;
using Xunit;

namespace Day12Tests
{
    public class NoteTests
    {
        [Theory]
        [MemberData(nameof(FromStringCases))]
        public void FromStringAndMatchesReturnsCorrectly(string note, IDictionary<long, PlantPot> plantPots, bool expectedMatches, bool expectedPlantPotNextGen)
        {
            var subject = Note.FromString(note);
            var actualMatches = subject.Matches(plantPots, 0);
            Assert.Equal(expectedMatches, actualMatches);
            Assert.Equal(expectedPlantPotNextGen, subject.WillContainPlantNextGeneration);
        }

        public static IEnumerable<object[]> FromStringCases()
        {
            var plantPots = new Dictionary<long, PlantPot>()
            {
                {-2, PlantPot.PotWithoutPlant},
                {-1, PlantPot.PotWithoutPlant},
                {0, PlantPot.PotWithoutPlant},
                {1, PlantPot.PotWithPlant},
                {2, PlantPot.PotWithPlant},
            };

            yield return new object[] {"...## => #", plantPots, true, true};
            yield return new object[] {"...## => .", plantPots, true, false};
            yield return new object[] {"#..## => .", plantPots, false, false};
        }
    }
}
