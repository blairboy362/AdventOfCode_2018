using System.Text.RegularExpressions;

namespace Day10
{
    public class Point
    {
        private static readonly Regex PointDescriptionPattern = new Regex(@"position=<([0-9\- ]+),([0-9\- ]+)> *velocity=<([0-9\- ]+),([0-9\- ]+)>");
        private readonly int _velocityX;
        private readonly int _velocityY;

        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public Point(int positionX, int positionY, int velocityX, int velocityY)
        {
            PositionX = positionX;
            PositionY = positionY;
            _velocityX = velocityX;
            _velocityY = velocityY;
        }

        public static Point FromString(string description)
        {
            var match = PointDescriptionPattern.Match(description);
            var point = new Point(
                int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value),
                int.Parse(match.Groups[4].Value));
            return point;
        }

        public void Tick(int count = 1)
        {
            PositionX += (_velocityX * count);
            PositionY += (_velocityY * count);
        }

        public void TickBack()
        {
            PositionX -= _velocityX;
            PositionY -= _velocityY;
        }
    }
}
