namespace Day05
{
    public class Unit
    {
        private enum UnitPolarity
        {
            Upper,
            Lower,
        }

        private readonly char _type;
        private readonly UnitPolarity _polarity;

        public Unit(char type)
        {
            _type = char.ToUpper(type);

            _polarity = char.IsUpper(type) ? UnitPolarity.Upper : UnitPolarity.Lower;
        }

        public bool ReactsWith(Unit other)
        {
            return _type.Equals(other._type) && _polarity != other._polarity;
        }

        public override string ToString()
        {
            return $"{_type}, {_polarity}";
        }

        public char ToChar()
        {
            return _polarity == UnitPolarity.Lower ? char.ToLower(_type) : _type;
        }
    }
}
