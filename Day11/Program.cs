using System;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var cellGrid = new FuelCellGrid(8868);
            var largestPower = cellGrid.GetSquareWithLargestPower(3);

            Console.WriteLine(
                "3x3 square with greatest power has top-left {0} and power {1}",
                largestPower.TopLeft,
                largestPower.Power);

            var mostPowerfulSquare = cellGrid.GetSquareWithLargestPower();

            Console.WriteLine(
                "Square with greatest power has top-left {0}, size {1} and power {2}",
                mostPowerfulSquare.TopLeft,
                mostPowerfulSquare.Size,
                mostPowerfulSquare.Power);
        }
    }
}
