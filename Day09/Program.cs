﻿using System;

namespace Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            var marbleMania = new MarbleMania(405, 7170000);
            marbleMania.Play();

            Console.WriteLine("Highest score: {0}", marbleMania.HighestScore);
        }
    }
}
