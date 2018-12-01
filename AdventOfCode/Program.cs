using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing frequency changes argument");
            }

            var calibrationFrequencies = LoadFromFile(args[0]);
            var calibrator = new Calibrator();
            var calibratedFrequency = calibrator.Calibrate(calibrationFrequencies);

            Console.WriteLine("Calibrated frequency: {0}", calibratedFrequency);
        }

        private static IEnumerable<Frequency> LoadFromFile(string path)
        {
            var calibrationFrequencies = new List<Frequency>();
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var calibrationFrequency = Frequency.FromString(line);
                    calibrationFrequencies.Add(calibrationFrequency);
                }
            }

            return calibrationFrequencies;
        }
    }
}
