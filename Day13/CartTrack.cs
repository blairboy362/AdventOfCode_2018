using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class CartTrack
    {
        private readonly IDictionary<Coordinates, char> _track;
        private readonly IEnumerable<Cart> _cartPositions;

        private CartTrack(IDictionary<Coordinates, char> track, IEnumerable<Cart> cartPositions)
        {
            _track = track;
            _cartPositions = cartPositions;
        }

        public static CartTrack FromStrings(IEnumerable<string> source)
        {
            var track = new Dictionary<Coordinates, char>();
            var cartPositions = new List<Cart>();

            var y = 0;

            foreach (var line in source)
            {
                var x = 0;

                foreach (var segment in line)
                {
                    var coordinates = new Coordinates(x, y);

                    switch (segment)
                    {
                        case '-':
                        case '|':
                        case '/':
                        case '\\':
                        case '+':
                            track.Add(coordinates, segment);
                            break;
                        case '^':
                            track.Add(coordinates, '|');
                            cartPositions.Add(new Cart(CartOrientation.Up, coordinates));
                            break;
                        case 'v':
                            track.Add(coordinates, '|');
                            cartPositions.Add(new Cart(CartOrientation.Down, coordinates));
                            break;
                        case '<':
                            track.Add(coordinates, '-');
                            cartPositions.Add(new Cart(CartOrientation.Left, coordinates));
                            break;
                        case '>':
                            track.Add(coordinates, '-');
                            cartPositions.Add(new Cart(CartOrientation.Right, coordinates));
                            break;
                    }
                    x++;
                }

                y++;
            }

            return new CartTrack(track, cartPositions);
        }

        public Coordinates FindCoordinatesOfFirstCrash()
        {
            while (true)
            {
                foreach (var cart in _cartPositions.OrderBy(c => c.Coordinates))
                {
                    cart.Tick(_track[cart.Coordinates]);
                    if (TickCausedCollision(cart.Coordinates))
                    {
                        return cart.Coordinates;
                    }
                }
            }
        }

        public Coordinates FindCoordinatesOfRemainingCart()
        {
            var cartsOnTrack = new HashSet<Cart>(_cartPositions);

            while (cartsOnTrack.Count > 1)
            {
                Tick(cartsOnTrack);
            }

            return cartsOnTrack.First().Coordinates;
        }

        private void Tick(ISet<Cart> cartsOnTrack)
        {
            var cartsToMoveThisTick = new HashSet<Cart>(cartsOnTrack);

            while (cartsToMoveThisTick.Count > 0)
            {
                IEnumerable<Cart> collidedCarts = new List<Cart>();
                var cartsMovedThisTick = new HashSet<Cart>();
                foreach (var cart in cartsToMoveThisTick.OrderBy(c => c.Coordinates))
                {
                    cart.Tick(_track[cart.Coordinates]);
                    cartsMovedThisTick.Add(cart);

                    if (cartsOnTrack.Count(c => c.Coordinates.Equals(cart.Coordinates)) > 1)
                    {
                        collidedCarts = cartsOnTrack.Where(c => c.Coordinates.Equals(cart.Coordinates)).ToHashSet();
                        break;
                    }
                }

                cartsOnTrack.ExceptWith(collidedCarts);
                cartsToMoveThisTick.IntersectWith(cartsOnTrack);
                cartsToMoveThisTick.ExceptWith(cartsMovedThisTick);
            }
        }

        private bool TickCausedCollision(Coordinates coordinates)
        {
            return _cartPositions.Count(c => c.Coordinates.Equals(coordinates)) > 1;
        }
    }
}
