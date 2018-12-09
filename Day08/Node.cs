using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    public class Node
    {
        public IList<Node> Children { get; }

        public ICollection<MetadataEntry> MetadataEntries { get; }

        public Node()
        {
            Children = new List<Node>();
            MetadataEntries = new List<MetadataEntry>();
        }

        public int Value
        {
            get
            {
                if (!Children.Any())
                {
                    return MetadataEntries.Sum(m => m.Value);
                }

                var cumulativeTotal = 0;
                foreach (var metadataEntry in MetadataEntries)
                {
                    var childNodeIndex = metadataEntry.Value - 1;
                    if (childNodeIndex >= 0 && childNodeIndex < Children.Count)
                    {
                        cumulativeTotal += Children[childNodeIndex].Value;
                    }
                }

                return cumulativeTotal;
            }
        }
    }
}
