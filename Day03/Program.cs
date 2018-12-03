using System;
using System.Collections.Generic;
using Utils;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing claims argument");
            }

            var claims = LoadFromFile(args[0]);
            var fabric = new FabricClaims(claims);
            var overlappingClaimCount = fabric.CountOverlappingClaims();

            Console.WriteLine("Overlapping claim count: {0}", overlappingClaimCount);

            var nonOverlappingClaims = fabric.FindNonOverlappingClaims();
            foreach (var nonOverlappingClaim in nonOverlappingClaims)
            {
                Console.WriteLine("Non-overlapping claim: {0}", nonOverlappingClaim);
            }
        }

        private static IEnumerable<Claim> LoadFromFile(string path)
        {
            var claims = new List<Claim>();

            FileHandling.Load(path, l =>
            {
                var claim = Claim.FromString(l);
                claims.Add(claim);
            });

            return claims;
        }
    }
}
