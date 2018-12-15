using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day09
{
    public class MarbleMania
    {
        private readonly IList<Player> _players;
        private readonly IList<Marble> _marbles;

        public long HighestScore
        {
            get { return _players.Max(p => p.Score); }
        }

        public MarbleMania(int playerCount, int lastMarble)
        {
            _players = new List<Player>();
            for (var i = 0; i < playerCount; i++)
            {
                _players.Add(new Player(i));
            }

            _marbles = new List<Marble>(lastMarble + 1);
            for (var i = 0; i <= lastMarble; i++)
            {
                _marbles.Add(new Marble(i));
            }
        }

        public void Play()
        {
            var marblesEnumerator = _marbles.GetEnumerator();
            if (!marblesEnumerator.MoveNext())
            {
                throw new InvalidOperationException("Unable to start game - no marbles!");
            }

            var circleOfMarbles = new LinkedList<Marble>();
            circleOfMarbles.AddFirst(marblesEnumerator.Current);
            var currentMarble = circleOfMarbles.First;

            foreach (var turn in new MarbleManiaGame(_players, marblesEnumerator))
            {
                currentMarble = turn.Take(currentMarble);
            }
        }

        private class Turn
        {
            public Player Player { get; }
            public Marble Marble { get; }

            public Turn(Player player, Marble marble)
            {
                Player = player;
                Marble = marble;
            }

            public LinkedListNode<Marble> Take(LinkedListNode<Marble> currentMarble)
            {
                if (!Marble.Counts())
                {
                    var newNode = new LinkedListNode<Marble>(Marble);
                    currentMarble.List.AddClockwiseOf(currentMarble.Clockwise(), newNode);
                    return newNode;
                }

                return Score(currentMarble);
            }

            private LinkedListNode<Marble> Score(LinkedListNode<Marble> currentMarble)
            {
                Player.AddToScore(Marble);
                var marbleToExtract = currentMarble;
                for (var i = 0; i < 7; i++)
                {
                    marbleToExtract = marbleToExtract.CounterClockwise();
                }

                var newCurrentMarble = marbleToExtract.Clockwise();
                marbleToExtract.List.Remove(marbleToExtract);
                Player.AddToScore(marbleToExtract.Value);

                return newCurrentMarble;
            }
        }

        private class MarbleManiaGame : IEnumerable<Turn>
        {
            private readonly IEnumerable<Player> _players;
            private readonly IEnumerator<Marble> _marbleEnumerator;

            public MarbleManiaGame(IEnumerable<Player> players, IEnumerator<Marble> marbleEnumerator)
            {
                _players = players;
                _marbleEnumerator = marbleEnumerator;
            }

            public IEnumerator<Turn> GetEnumerator()
            {
                return new MarbleManiaTurnEnumerator(_players, _marbleEnumerator);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class MarbleManiaTurnEnumerator : IEnumerator<Turn>
        {
            private readonly IEnumerable<Player> _players;
            private readonly IEnumerator<Marble> _marbleEnumerator;
            private IEnumerator<Player> _playerEnumerator;

            public MarbleManiaTurnEnumerator(IEnumerable<Player> players, IEnumerator<Marble> marbleEnumerator)
            {
                _players = players;
                _marbleEnumerator = marbleEnumerator;
                _playerEnumerator = InitialisePlayerEnumerator();
            }

            public bool MoveNext()
            {
                if (!_marbleEnumerator.MoveNext())
                {
                    return false;
                }

                if (!_playerEnumerator.MoveNext())
                {
                    _playerEnumerator.Dispose();
                    _playerEnumerator = InitialisePlayerEnumerator();
                    if (!_playerEnumerator.MoveNext())
                    {
                        throw new InvalidOperationException("Failed to re-initialise player enumerator");
                    }
                }

                Current = new Turn(_playerEnumerator.Current, _marbleEnumerator.Current);

                return true;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public Turn Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            private IEnumerator<Player> InitialisePlayerEnumerator()
            {
                return _players.GetEnumerator();
            }
        }
    }
}
