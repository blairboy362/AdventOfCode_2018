using System;
using System.Collections.Generic;

namespace Day13
{
    public class Cart
    {
        private static readonly IDictionary<Tuple<CartOrientation, char>, Func<Coordinates, Tuple<Coordinates, CartOrientation>>> AvailableActions;
        private static readonly IDictionary<Tuple<CartOrientation, IntersectionDirection>, char> IntersectionTranslations;
        private static readonly LinkedList<IntersectionDirection> IntersectionDirections;

        public CartOrientation Orientation { get; private set; }
        public Coordinates Coordinates { get; private set; }

        private Guid _id;
        private LinkedListNode<IntersectionDirection> _intersectionDirection;

        static Cart()
        {
            AvailableActions = BuildAvailableActions();
            IntersectionTranslations = BuildIntersectionLookupTable();
            IntersectionDirections = new LinkedList<IntersectionDirection>();
            IntersectionDirections.AddLast(IntersectionDirection.Left);
            IntersectionDirections.AddLast(IntersectionDirection.Straight);
            IntersectionDirections.AddLast(IntersectionDirection.Right);
        }

        public Cart(CartOrientation orientation, Coordinates initialCoordinates)
        {
            _id = Guid.NewGuid();
            Orientation = orientation;
            Coordinates = initialCoordinates;
            _intersectionDirection = IntersectionDirections.First;
        }

        public void Tick(char mapSegment)
        {
            var translatedMapSegment = mapSegment;
            if (mapSegment == '+')
            {
                translatedMapSegment = IntersectionTranslations[
                    new Tuple<CartOrientation, IntersectionDirection>(Orientation, _intersectionDirection.Value)];
                _intersectionDirection = _intersectionDirection.Next();
            }
            var action = AvailableActions[new Tuple<CartOrientation, char>(Orientation, translatedMapSegment)];
            var result = action(Coordinates);
            Coordinates = result.Item1;
            Orientation = result.Item2;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cart) obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        protected bool Equals(Cart other)
        {
            return _id.Equals(other._id);
        }

        private static IDictionary<Tuple<CartOrientation, char>, Func<Coordinates, Tuple<Coordinates, CartOrientation>>> BuildAvailableActions()
        {
            var availableActions = new Dictionary<Tuple<CartOrientation, char>, Func<Coordinates, Tuple<Coordinates, CartOrientation>>>();

            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Right, '-'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X + 1, coordinates.Y), CartOrientation.Right));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Left, '-'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X - 1, coordinates.Y), CartOrientation.Left));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Up, '|'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X, coordinates.Y - 1), CartOrientation.Up));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Down, '|'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X, coordinates.Y + 1), CartOrientation.Down));

            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Right, '/'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X, coordinates.Y - 1), CartOrientation.Up));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Left, '/'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X, coordinates.Y + 1), CartOrientation.Down));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Up, '/'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X + 1, coordinates.Y), CartOrientation.Right));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Down, '/'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X - 1, coordinates.Y), CartOrientation.Left));

            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Right, '\\'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X, coordinates.Y + 1), CartOrientation.Down));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Left, '\\'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X, coordinates.Y - 1), CartOrientation.Up));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Up, '\\'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X - 1, coordinates.Y), CartOrientation.Left));
            availableActions.Add(new Tuple<CartOrientation, char>(CartOrientation.Down, '\\'), (coordinates) => new Tuple<Coordinates, CartOrientation>(new Coordinates(coordinates.X + 1, coordinates.Y), CartOrientation.Right));

            return availableActions;
        }

        private static IDictionary<Tuple<CartOrientation, IntersectionDirection>, char> BuildIntersectionLookupTable()
        {
            var lookupTable = new Dictionary<Tuple<CartOrientation, IntersectionDirection>, char>();

            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Right, IntersectionDirection.Left), '/');
            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Right, IntersectionDirection.Straight), '-');
            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Right, IntersectionDirection.Right), '\\');

            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Left, IntersectionDirection.Left), '/');
            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Left, IntersectionDirection.Straight), '-');
            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Left, IntersectionDirection.Right), '\\');

            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Up, IntersectionDirection.Left), '\\');
            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Up, IntersectionDirection.Straight), '|');
            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Up, IntersectionDirection.Right), '/');

            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Down, IntersectionDirection.Left), '\\');
            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Down, IntersectionDirection.Straight), '|');
            lookupTable.Add(new Tuple<CartOrientation, IntersectionDirection>(CartOrientation.Down, IntersectionDirection.Right), '/');

            return lookupTable;
        }
    }
}
