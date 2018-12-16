using System;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var cellGrid = new FuelCellGrid(8868, 300, 300);
            var largestPower = cellGrid.LargestPower;

            Console.WriteLine(
                "Cell with greatest power has top-left {0} and power {1}",
                largestPower.TopLeft,
                largestPower.Power);
        }
    }
}
