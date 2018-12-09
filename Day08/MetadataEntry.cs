namespace Day08
{
    public class MetadataEntry
    {
        public int Value { get; }

        public MetadataEntry(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
