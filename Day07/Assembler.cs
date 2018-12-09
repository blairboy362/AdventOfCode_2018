using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Day07
{
    public class Assembler
    {
        private static readonly Regex InstructionPattern =
            new Regex(@"Step ([A-Z]) must be finished before step ([A-Z]) can begin.");

        private readonly IEnumerable<Step> _instructionGraph;

        public Assembler(IEnumerable<string> instructions, int baselineDuration = 0)
        {
            _instructionGraph = ParseInstructions(instructions, baselineDuration);
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

        public int AssembleWithMultipleWorkers(int workerCount)
        {
            var workers = new List<Worker>(workerCount);
            for (int i = 0; i < workerCount; i++)
            {
                workers.Add(new Worker(i));
            }

            var totalTicks = 0;
            while (_instructionGraph.Any(s => !s.Completed))
            {
                AssignWork(workers);
                foreach (var worker in workers)
                {
                    worker.Tick();
                }
                totalTicks++;

                CompleteWork(workers);
            }

            return totalTicks;
        }

        private static IEnumerable<Step> ParseInstructions(IEnumerable<string> instructions, int baselineDuration)
        {
            var instructionGraph = new HashSet<Step>();

            foreach (var instruction in instructions)
            {
                var match = InstructionPattern.Match(instruction);
                if (!match.Success)
                {
                    throw new InvalidOperationException(string.Format("Failed to match instruction {0}", instruction));
                }

                var prerequisite = new Step(match.Groups[1].Value[0], baselineDuration);
                var current = new Step(match.Groups[2].Value[0], baselineDuration);

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

        private Step GetNextStep()
        {
            return _instructionGraph
                .Where(s => s.CanStart())
                .ToArray()
                .OrderBy(s => s)
                .FirstOrDefault();
        }

        private void AssignWork(IList<Worker> workers)
        {
            var nextStep = GetNextStep();
            while (nextStep != default(Step) && workers.Any(w => w.CanAssignWork))
            {
                workers.First(w => w.CanAssignWork).AssignWork(nextStep);
                nextStep = GetNextStep();
            }
        }

        private void CompleteWork(IList<Worker> workers)
        {
            var workersWithCompletedWork = workers
                .Where(w => w.Completed)
                .OrderBy(w => w.CurrentStep);
            foreach (var worker in workersWithCompletedWork)
            {
                worker.CurrentStep.Complete();
                worker.Reset();
            }
        }
    }
}
