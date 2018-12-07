using System.Collections.Generic;
using System.Text;

namespace Day05
{
    public class Polymer
    {
        private readonly IList<Unit> _allUnits;

        public Polymer(IList<Unit> allUnits)
        {
            _allUnits = allUnits;
        }

        public string Activate()
        {
            var remainingUnits = new List<Unit>(_allUnits);
            var reacted = true;

            while (reacted)
            {
                reacted = false;
                var indexToPrune = -1;

                for (var i = 0; i < remainingUnits.Count - 1 && indexToPrune == -1; i++)
                {
                    if (remainingUnits[i].ReactsWith(remainingUnits[i + 1]))
                    {
                        indexToPrune = i;
                    }
                }

                if (indexToPrune >= 0)
                {
                    reacted = true;

                    remainingUnits.RemoveAt(indexToPrune);
                    remainingUnits.RemoveAt(indexToPrune);
                }
            }

            var remainingPolymer = new StringBuilder();
            foreach (var unit in remainingUnits)
            {
                remainingPolymer.Append(unit.ToChar());
            }

            return remainingPolymer.ToString();
        }
    }
}
