using System;
using System.IO;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing instructions argument");
            }

            var rawLicence = LoadFromFile(args[0]);
            var licence = Licence.FromString(rawLicence);
            var metadataSum = licence.MetadataSum;
            Console.WriteLine("Metadata sum: {0}", metadataSum);

            var rootNodeValue = licence.RootNodeValue;
            Console.WriteLine("Root node value: {0}", rootNodeValue);
        }

        private static string LoadFromFile(string path)
        {
            return File.ReadAllText(path).Trim();
        }
    }
}
