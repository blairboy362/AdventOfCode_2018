using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    public class FuelCellGrid
    {
        private readonly IDictionary<Coordinates, FuelCell> _allFuelCells;
        private readonly IEnumerable<CellSquare> _squares;

        public CellSquare LargestPower
        {
            get { return _squares.OrderByDescending(s => s.Power).First(); }
        }

        public FuelCellGrid(int serialNumber, int width, int height)
        {
            _allFuelCells = GenerateCells(serialNumber, width, height);
            _squares = PopulateSquares(width, height);
        }

        private static IDictionary<Coordinates, FuelCell> GenerateCells(int serialNumber, int width, int height)
        {
            var cells = new Dictionary<Coordinates, FuelCell>();

            for (var x = 1; x <= width; x++)
            {
                for (var y = 1; y <= height; y++)
                {
                    var cellCoordinates = new Coordinates(x, y);
                    cells.Add(cellCoordinates, new FuelCell(cellCoordinates, serialNumber));
                }
            }

            return cells;
        }

        private IEnumerable<CellSquare> PopulateSquares(int width, int height)
        {
            var squares = new List<CellSquare>();

            for (var x = 1; x <= width - 2; x++)
            {
                for (var y = 1; y <= height - 2; y++)
                {
                    squares.Add(new CellSquare(new Coordinates(x, y), _allFuelCells));
                }
            }

            return squares;
        }
    }
}
