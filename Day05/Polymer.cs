using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return Activate((a) => false);
        }

        public string ActivateShortest()
        {
            var histogram = new Dictionary<Unit, string>();
            var problemCandidates = new HashSet<Unit>();

            for (var c = 'A'; c <= 'Z'; c++)
            {
                problemCandidates.Add(new Unit(c));
            }

            var histogramLock = new object();
            Parallel.ForEach(problemCandidates, (problemCandidate) =>
            {
                var suitPolymer = Activate((a) => a.EqualsIgnorePolarity(problemCandidate));

                lock (histogramLock)
                {
                    histogram[problemCandidate] = suitPolymer;
                }
            });

            return histogram.OrderBy(p => p.Value.Length).First().Value;
        }

        private string Activate(Func<Unit, bool> shouldRemove)
        {
            var remainingUnits = new List<Unit>(_allUnits);

            for (var i = 0; i < remainingUnits.Count; i++)
            {
                if (shouldRemove(remainingUnits[i]))
                {
                    remainingUnits.RemoveAt(i);
                    i--;
                }
            }

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
