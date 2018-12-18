namespace Day12
{
    public class PlantPot
    {
        public static readonly PlantPot PotWithPlant = new PlantPot(true);
        public static readonly PlantPot PotWithoutPlant = new PlantPot(false);

        public bool HasPlant { get; }

        private PlantPot(bool hasPlant)
        {
            HasPlant = hasPlant;
        }

        public override string ToString()
        {
            return string.Format("{0}", HasPlant ? '#' : '.');
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlantPot) obj);
        }

        public override int GetHashCode()
        {
            return HasPlant.GetHashCode();
        }

        protected bool Equals(PlantPot other)
        {
            return HasPlant == other.HasPlant;
        }
    }
}
