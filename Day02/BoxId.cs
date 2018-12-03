using System.Collections.Generic;
using System.Text;

namespace Day02
{
    public class BoxId
    {
        private readonly string _boxId;
        private readonly IDictionary<char, int> _characterCounts;

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

        public bool Similar(BoxId other)
        {
            if (Equals(other)) return false;
            if (_boxId.Length != other._boxId.Length) return false;

            var differences = 0;

            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var i = 0; i < _boxId.Length; i++)
            {
                if (_boxId[i] != other._boxId[i])
                {
                    differences++;
                }
            }

            return differences == 1;
        }

        public string CommonCharacters(BoxId other)
        {
            var common = new StringBuilder();

            for (var i = 0; i < _boxId.Length; i++)
            {
                if (_boxId[i] == other._boxId[i])
                {
                    common.Append(_boxId[i]);
                }
            }

            return common.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BoxId) obj);
        }

        public override int GetHashCode()
        {
            return (_boxId != null ? _boxId.GetHashCode() : 0);
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
                // ReSharper disable once SwitchStatementMissingSomeCases
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

        private bool Equals(BoxId other)
        {
            return string.Equals(_boxId, other._boxId);
        }
    }
}
