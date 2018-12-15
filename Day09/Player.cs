namespace Day09
{
    public class Player
    {
        private readonly int _id;

        public long Score { get; private set; }

        public Player(int id)
        {
            _id = id;
            Score = 0;
        }

        public void AddToScore(Marble marble)
        {
            Score += marble.Value;
        }

        public override string ToString()
        {
            return $"{_id}: {Score}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            return _id;
        }

        protected bool Equals(Player other)
        {
            return _id == other._id;
        }
    }
}
