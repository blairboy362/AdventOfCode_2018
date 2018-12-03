using System.Collections.Generic;

namespace Day02
{
    public class BoxId
    {
        private readonly string _boxId;
        private IDictionary<char, int> _characterCounts;

        public BoxId(string boxId)
        {
            _boxId = boxId.ToLower();
            _characterCounts = new Dictionary<char, int>();
            CountCharacters();
            SetFlags();
        }

        public bool ContainsExactlyTwo
        {
            get;
            private set;
        }

        public bool ContainsExactlyThree
        {
            get;
            private set;
        }

        private void CountCharacters()
        {
            foreach (var character in _boxId.ToCharArray())
            {
                if (!_characterCounts.ContainsKey(character))
                {
                    _characterCounts[character] = 1;
                }
                else
                {
                    _characterCounts[character]++;
                }
            }
        }

        private void SetFlags()
        {
            foreach (var entry in _characterCounts)
            {
                switch (entry.Value)
                {
                    case 2:
                        ContainsExactlyTwo = true;
                        continue;
                    case 3:
                        ContainsExactlyThree = true;
                        continue;
                }
            }
        }
    }
}
