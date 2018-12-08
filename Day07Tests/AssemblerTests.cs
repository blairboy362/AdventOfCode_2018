using System.Collections.Generic;
using Day07;
using Xunit;

namespace Day07Tests
{
    public class AssemblerTests
    {
        [Theory]
        [MemberData(nameof(AssembleCases))]
        public void AssembleCompletesCorrectly(IEnumerable<string> instructions, string expected)
        {
            var subject = new Assembler(instructions);
            var actual = subject.Assemble();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> AssembleCases()
        {
            var instructions = new string[]
            {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
                "Step A must be finished before step D can begin.",
                "Step B must be finished before step E can begin.",
                "Step D must be finished before step E can begin.",
                "Step F must be finished before step E can begin.",
            };
            yield return new object[] {instructions, "CABDFE"};
        }
    }
}
