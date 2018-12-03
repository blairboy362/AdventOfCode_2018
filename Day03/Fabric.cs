using System.Collections.Generic;
using System.Linq;

namespace Day03
{
    public class Fabric
    {
        public int CountOverlappingClaims(IEnumerable<Claim> claims)
        {
            var claimMap = new Dictionary<Coordinates, int>();

            foreach (var claim in claims)
            {
                for (var x = claim.TopLeft.X; x < claim.TopRight.X; x++)
                {
                    for (var y = claim.TopLeft.Y; y < claim.BottomLeft.Y; y++)
                    {
                        var coordinates = new Coordinates(x, y);
                        if (!claimMap.ContainsKey(coordinates))
                        {
                            claimMap[coordinates] = 1;
                        }
                        else
                        {
                            claimMap[coordinates]++;
                        }
                    }
                }
            }

            return claimMap.Values.Count(c => c > 1);;
        }
    }
}
