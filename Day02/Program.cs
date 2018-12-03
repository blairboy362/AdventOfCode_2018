using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing box ids argument");
            }

            var boxIds = LoadFromFile(args[0]);
            var prototypeFabricLocator = new PrototypeFabricLocator();
            var checksum = prototypeFabricLocator.CalculateChecksum(boxIds);

            Console.WriteLine("Checksum: {0}", checksum);

            var possibleLocations = prototypeFabricLocator.FindSimilarBoxes(boxIds);
            foreach (var possibleLocation in possibleLocations)
            {
                Console.WriteLine("Possible location: {0}", possibleLocation);
            }
        }

        private static IList<BoxId> LoadFromFile(string path)
        {
            var boxIds = new HashSet<BoxId>();
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var boxId = new BoxId(line);
                    boxIds.Add(boxId);
                }
            }

            return boxIds.ToList();
        }
    }
}
