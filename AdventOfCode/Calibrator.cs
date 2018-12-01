using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Calibrator
    {
        public Frequency Calibrate(IEnumerable<Frequency> calibrationFrequencies)
        {
            if (calibrationFrequencies == null)
            {
                throw new ArgumentNullException(nameof(calibrationFrequencies));
            }

            var calibratedFrequency = new Frequency(0);

            foreach (var frequencyDelta in calibrationFrequencies)
            {
                calibratedFrequency = calibratedFrequency + frequencyDelta;
            }

            return calibratedFrequency;
        }
    }
}