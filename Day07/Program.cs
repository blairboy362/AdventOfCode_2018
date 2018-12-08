using System;
using System.Collections.Generic;
using Utils;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Missing instructions argument");
            }

            var instructions = LoadFromFile(args[0]);
            var assembler = new Assembler(instructions);
            var orderOfAssembly = assembler.Assemble();
            Console.WriteLine("Order of assembly is: {0}", orderOfAssembly);
        }

        private static IEnumerable<string> LoadFromFile(string path)
        {
            var instructions = new List<string>();

            FileHandling.Load(path, l => { instructions.Add(l); });

            return instructions;
        }
    }
}
