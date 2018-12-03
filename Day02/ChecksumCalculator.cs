using System.Collections.Generic;

namespace Day02
{
    public class ChecksumCalculator
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
    }
}
