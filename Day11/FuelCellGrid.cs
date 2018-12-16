using System.Collections.Generic;

namespace Day11
{
    public class FuelCellGrid
    {
        private const int Width = 300;
        private const int Height = 300;
        private IDictionary<Coordinates, FuelCell> _allFuelCells;
        private IDictionary<Coordinates, long> _summedAreaTable;

        public FuelCellGrid(int serialNumber)
        {
            GenerateCells(serialNumber);
        }

        public CellSquare GetSquareWithLargestPower(int size)
        {
            return GetMostPowerfulSquare(size);
        }

        public CellSquare GetSquareWithLargestPower()
        {
            var currentLargestSquare = new CellSquare(new Coordinates(1, 1), _summedAreaTable, 1);

            for (var size = 1; size <= 300; size++)
            {
                var candidateSquare = GetMostPowerfulSquare(size);
                if (candidateSquare.Power > currentLargestSquare.Power)
                {
                    currentLargestSquare = candidateSquare;
                }
            }

            return currentLargestSquare;
        }

        private CellSquare GetMostPowerfulSquare(int size)
        {
            var currentLargestSquare = new CellSquare(new Coordinates(1, 1), _summedAreaTable, 1);

            for (var x = 1; x <= Width - size + 1; x++)
            {
                for (var y = 1; y <= Height - size + 1; y++)
                {
                    var candidateSquare = new CellSquare(new Coordinates(x, y), _summedAreaTable, size);
                    if (candidateSquare.Power > currentLargestSquare.Power)
                    {
                        currentLargestSquare = candidateSquare;
                    }
                }
            }

            return currentLargestSquare;
        }

        private void GenerateCells(int serialNumber)
        {
            _allFuelCells = new Dictionary<Coordinates, FuelCell>();
            _summedAreaTable = new Dictionary<Coordinates, long>();

            for (var x = 1; x <= Width; x++)
            {
                for (var y = 1; y <= Height; y++)
                {
                    var cellCoordinates = new Coordinates(x, y);
                    var cell = new FuelCell(cellCoordinates, serialNumber);
                    _allFuelCells.Add(cellCoordinates, cell);

                    var summedAreaPower = cell.Power +
                        GetSummedAreaForCoordinates(new Coordinates(cellCoordinates.X, cellCoordinates.Y - 1)) +
                        GetSummedAreaForCoordinates(new Coordinates(cellCoordinates.X - 1, cellCoordinates.Y)) -
                        GetSummedAreaForCoordinates(new Coordinates(cellCoordinates.X - 1, cellCoordinates.Y - 1));
                    _summedAreaTable.Add(cellCoordinates, summedAreaPower);
                }
            }
        }

        private long GetSummedAreaForCoordinates(Coordinates coordinates)
        {
            return _summedAreaTable.ContainsKey(coordinates) ? _summedAreaTable[coordinates] : 0;
        }
    }
}
