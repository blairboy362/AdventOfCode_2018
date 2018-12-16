namespace Day11
{
    public class FuelCell
    {
        private readonly Coordinates _coordinates;

        public long Power { get; }

        public FuelCell(Coordinates coordinates, int gridSerialNumber)
        {
            _coordinates = coordinates;
            Power = CalculatePower(gridSerialNumber);
        }

        public override string ToString()
        {
            return $"{_coordinates}: {Power}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FuelCell) obj);
        }

        public override int GetHashCode()
        {
            return _coordinates.GetHashCode();
        }

        protected bool Equals(FuelCell other)
        {
            return _coordinates.Equals(other._coordinates);
        }

        private long CalculatePower(int gridSerialNumber)
        {
            var rackId = (long)(_coordinates.X + 10);
            var power = rackId * _coordinates.Y;
            power += gridSerialNumber;
            power *= rackId;
            var hundreds = (power / 100)  * 100;
            var thousands = (power / 1000) * 1000;
            power = (hundreds - thousands) / 100;
            power -= 5;
            return power;
        }
    }
}
