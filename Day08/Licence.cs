using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    public class Licence
    {
        private readonly IEnumerable<Node> _nodes;

        private Licence(IEnumerable<Node> nodes)
        {
            _nodes = nodes;
        }

        public static Licence FromString(string licence)
        {
            var rawTokens = ParseRawTokens(licence);
            var licenceReader = new LicenceReader(rawTokens);
            return new Licence(licenceReader.Read());
        }

        public int MetadataSum
        {
            get
            {
                return _nodes
                    .SelectMany(n => n.MetadataEntries)
                    .Sum(m => m.Value);
            }
        }

        public int RootNodeValue => _nodes.First().Value;

        private static IEnumerable<int> ParseRawTokens(string licence)
        {
            var tokens = new List<int>();

            foreach (var token in licence.Split(' '))
            {
                tokens.Add(int.Parse(token));
            }

            return tokens;
        }

        private class LicenceReader
        {
            private IEnumerable<int> _rawTokens;
            private readonly IEnumerator<int> _tokenEnumerator;

            public LicenceReader(IEnumerable<int> rawTokens)
            {
                _rawTokens = rawTokens;
                _tokenEnumerator = _rawTokens.GetEnumerator();
            }

            public IEnumerable<Node> Read()
            {
                var nodes = new List<Node>();
                var root = new Node();
                nodes.Add(root);

                ReadNodesRecurse(nodes, root);

                return nodes;
            }

            private void ReadNodesRecurse(ICollection<Node> nodes, Node parent)
            {
                if (!_tokenEnumerator.MoveNext())
                {
                    return;
                }

                var childNodeCount = _tokenEnumerator.Current;

                if (!_tokenEnumerator.MoveNext())
                {
                    throw new InvalidOperationException("Incomplete header in raw token!");
                }

                var metadataEntryCount = _tokenEnumerator.Current;

                for (var c = 0; c < childNodeCount; c++)
                {
                    var child = new Node();
                    nodes.Add(child);
                    parent.Children.Add(child);
                    ReadNodesRecurse(nodes, child);
                }

                for (var m = 0; m < metadataEntryCount; m++)
                {
                    if (!_tokenEnumerator.MoveNext())
                    {
                        throw new InvalidOperationException("Failed to read expected metadata entry!");
                    }

                    var metadataEntry = new MetadataEntry(_tokenEnumerator.Current);
                    parent.MetadataEntries.Add(metadataEntry);
                }
            }
        }
    }
}
