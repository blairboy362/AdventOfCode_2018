namespace Day09
{
    public class Marble
    {
        public int Value { get; }

        public Marble(int value)
        {
            Value = value;
        }

        public bool Counts()
        {
            if (Value == 0)
            {
                return false;
            }

            return Value % 23 == 0;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Marble) obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        protected bool Equals(Marble other)
        {
            return Value == other.Value;
        }
    }
}
