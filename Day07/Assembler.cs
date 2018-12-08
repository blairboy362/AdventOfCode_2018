using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day07
{
    public class Assembler
    {
        private static readonly Regex InstructionPattern = new Regex(@"Step ([A-Z]) must be finished before step ([A-Z]) can begin.");

        private readonly IEnumerable<Step> _instructionGraph;

        public Assembler(IEnumerable<string> instructions)
        {
            _instructionGraph = ParseInstructions(instructions);
        }

        public string Assemble()
        {
            var remainingInstructions = _instructionGraph
                .Where(s => s.CanStart())
                .ToArray();
            var orderOfCompletion = new StringBuilder();
            while (remainingInstructions.Any())
            {
                var nextStep = remainingInstructions
                    .OrderBy(s => s)
                    .First();
                orderOfCompletion.Append(nextStep.Complete());

                remainingInstructions = _instructionGraph
                    .Where(s => s.CanStart())
                    .ToArray();
            }

            return orderOfCompletion.ToString();
        }

        private static IEnumerable<Step> ParseInstructions(IEnumerable<string> instructions)
        {
            var instructionGraph = new HashSet<Step>();

            foreach (var instruction in instructions)
            {
                var match = InstructionPattern.Match(instruction);
                if (!match.Success)
                {
                    throw new InvalidOperationException(string.Format("Failed to match instruction {0}", instruction));
                }

                var prerequisite = new Step(match.Groups[1].Value[0]);
                var current = new Step(match.Groups[2].Value[0]);

                if (instructionGraph.TryGetValue(prerequisite, out var capture))
                {
                    prerequisite = capture;
                }
                else
                {
                    instructionGraph.Add(prerequisite);
                }

                if (instructionGraph.TryGetValue(current, out capture))
                {
                    current = capture;
                }
                else
                {
                    instructionGraph.Add(current);
                }

                current.AddPrerequisite(prerequisite);
            }

            return instructionGraph;
        }
    }
}
