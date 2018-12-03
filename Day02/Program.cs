using System;
using System.Collections.Generic;
using System.IO;
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
            var checksumCalculator = new ChecksumCalculator();
            var checksum = checksumCalculator.CalculateChecksum(boxIds);

            Console.WriteLine("Checksum: {0}", checksum);
        }

        private static IEnumerable<BoxId> LoadFromFile(string path)
        {
            var boxIds = new List<BoxId>();
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

            return boxIds;
        }
    }
}
