using System.Collections.Generic;

namespace Day08
{
    public class Node
    {
        public ICollection<Node> Children { get; }

        public ICollection<MetadataEntry> MetadataEntries { get; }

        public Node()
        {
            Children = new List<Node>();
            MetadataEntries = new List<MetadataEntry>();
        }
    }
}
