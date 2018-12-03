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
            var fabric = new Fabric();
            var overlappingClaimCount = fabric.CountOverlappingClaims(claims);

            Console.WriteLine("Overlapping claim count: {0}", overlappingClaimCount);
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
