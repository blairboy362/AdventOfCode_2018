using System.Collections.Generic;
using System.Linq;

namespace Day03
{
    public class FabricClaims
    {
        private readonly IEnumerable<Claim> _claims;
        private readonly IDictionary<Coordinates, int> _claimMap;

        public FabricClaims(IEnumerable<Claim> claims)
        {
            _claims = claims;
            _claimMap = BuildClaimMap(_claims);
        }

        public int CountOverlappingClaims()
        {
            return _claimMap.Values.Count(c => c > 1);
        }

        public IEnumerable<Claim> FindNonOverlappingClaims()
        {
            var nonOverlappingClaims = new HashSet<Claim>();
            var nonOverlappingCoordinates = _claimMap
                .Where(m => m.Value == 1)
                .Select(m => m.Key)
                .ToHashSet();

            foreach (var claim in _claims)
            {
                if (claim.OccupyingCoordinates.All(coordinates => nonOverlappingCoordinates.Contains(coordinates)))
                {
                    nonOverlappingClaims.Add(claim);
                }
            }

            return nonOverlappingClaims;
        }

        private static Dictionary<Coordinates, int> BuildClaimMap(IEnumerable<Claim> claims)
        {
            var claimMap = new Dictionary<Coordinates, int>();

            foreach (var claim in claims)
            {
                foreach (var coordinates in claim.OccupyingCoordinates)
                {
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

            return claimMap;
        }
    }
}
