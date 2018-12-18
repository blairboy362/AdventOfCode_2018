using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day12
{
    public class Note
    {
        private static readonly Regex NotePattern = new Regex(@"([\.#]{5}) => ([\.#]{1})");

        private readonly IDictionary<int, PlantPot> _rules;

        public bool WillContainPlantNextGeneration { get; }

        public Note(IDictionary<int, PlantPot> rules, bool willContainPlantNextGeneration)
        {
            _rules = rules;
            WillContainPlantNextGeneration = willContainPlantNextGeneration;
        }

        public bool Matches(IDictionary<int, PlantPot> plantPots, int currentPosition)
        {
            for (var i = -2; i <= 2; i++)
            {
                try
                {
                    if (!plantPots[currentPosition + i].Equals(_rules[i]))
                    {
                        return false;
                    }
                }
                catch (KeyNotFoundException)
                {
                    if (!PlantPot.PotWithoutPlant.Equals(_rules[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static Note FromString(string subject)
        {
            var match = NotePattern.Match(subject);
            if (!match.Success)
            {
                var msg = string.Format("Failed to match note from '{0}'", subject);
                throw new InvalidOperationException(msg);
            }

            var rules = new Dictionary<int, PlantPot>();

            for (var i = 0; i < 5; i++)
            {
                rules.Add(i - 2, match.Groups[1].Value[i] == '#' ? PlantPot.PotWithPlant : PlantPot.PotWithoutPlant);
            }

            return new Note(rules, match.Groups[2].Value[0] == '#');
        }
    }
}
