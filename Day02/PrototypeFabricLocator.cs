using System.Collections.Generic;

namespace Day02
{
    public class PrototypeFabricLocator
    {
        public int CalculateChecksum(IEnumerable<BoxId> boxIds)
        {
            var twos = 0;
            var threes = 0;

            foreach (var boxId in boxIds)
            {
                if (boxId.ContainsExactlyTwo)
                {
                    twos++;
                }

                if (boxId.ContainsExactlyThree)
                {
                    threes++;
                }
            }

            return twos * threes;
        }

        public IEnumerable<string> FindSimilarBoxes(IList<BoxId> boxIds)
        {
            var similarBoxes = new HashSet<string>();

            for (var i = 0; i < boxIds.Count; i++)
            {
                for (var j = i + 1; j < boxIds.Count; j++)
                {
                    if (boxIds[i].Similar(boxIds[j]))
                    {
                        var commonCharacters = boxIds[i].CommonCharacters(boxIds[j]);
                        similarBoxes.Add(commonCharacters);
                    }
                }
            }

            return similarBoxes;
        }
    }
}
