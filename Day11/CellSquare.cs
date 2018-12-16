using System.Collections.Generic;

namespace Day11
{
    public class CellSquare
    {
        public Coordinates TopLeft { get; }
        public int Size { get; }

        public long Power { get; private set; }

        public CellSquare(Coordinates topLeft, IDictionary<Coordinates, long> summedAreaTable, int size)
        {
            TopLeft = topLeft;
            Size = size;
            CalculatePower(summedAreaTable);
        }

        private void CalculatePower(IDictionary<Coordinates, long> summedAreaTable)
        {
            Power = GetSummedAreaForCoordinates(new Coordinates(TopLeft.X - 1, TopLeft.Y - 1), summedAreaTable) +
                GetSummedAreaForCoordinates(new Coordinates(TopLeft.X + Size - 1, TopLeft.Y + Size - 1), summedAreaTable) -
                GetSummedAreaForCoordinates(new Coordinates(TopLeft.X + Size - 1, TopLeft.Y - 1), summedAreaTable) -
                GetSummedAreaForCoordinates(new Coordinates(TopLeft.X - 1, TopLeft.Y + Size - 1), summedAreaTable);
        }

        private static long GetSummedAreaForCoordinates(Coordinates coordinates, IDictionary<Coordinates, long> summedAreaTable)
        {
            return summedAreaTable.ContainsKey(coordinates) ? summedAreaTable[coordinates] : 0;
        }
    }
}
