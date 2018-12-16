using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    public class CellSquare
    {
        private readonly ICollection<FuelCell> _cells;

        public Coordinates TopLeft { get; }

        public int Power
        {
            get { return _cells.Sum(c => c.Power); }
        }

        public CellSquare(Coordinates topLeft, IDictionary<Coordinates, FuelCell> fuelCells)
        {
            TopLeft = topLeft;
            _cells = AssignCells(fuelCells);
        }

        private ICollection<FuelCell> AssignCells(IDictionary<Coordinates, FuelCell> fuelCells)
        {
            var cells = new List<FuelCell>(9);
            for (var x = TopLeft.X; x <= TopLeft.X + 2; x++)
            {
                for (var y = TopLeft.Y; y <= TopLeft.Y + 2; y++)
                {
                    var coordinates = new Coordinates(x, y);
                    cells.Add(fuelCells[coordinates]);
                }
            }

            return cells;
        }
    }
}
