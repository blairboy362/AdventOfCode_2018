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
            var frequenciesAlreadyUsed = new HashSet<Frequency>();
            var foundCalibrationFrequency = false;
            frequenciesAlreadyUsed.Add(calibratedFrequency);

            while (!foundCalibrationFrequency)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var frequencyDelta in calibrationFrequencies)
                {
                    calibratedFrequency = calibratedFrequency + frequencyDelta;

                    if (frequenciesAlreadyUsed.Contains(calibratedFrequency))
                    {
                        foundCalibrationFrequency = true;
                        break;
                    }
                    else
                    {
                        frequenciesAlreadyUsed.Add(calibratedFrequency);
                    }
                }
            }

            return calibratedFrequency;
        }
    }
}
