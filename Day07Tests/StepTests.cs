using System;
using System.Collections.Generic;
using Day07;
using Xunit;

namespace Day07Tests
{
    public class StepTests
    {
        [Fact]
        public void CompleteCorrectlyUpdatesState()
        {
            var subject = new Step('A');
            Assert.False(subject.Completed);
            var result = subject.Complete();
            Assert.Equal('A', result);
            Assert.True(subject.Completed);
        }

        [Fact]
        public void CompleteThrowsOnIncompletePrerequisites()
        {
            var subject = new Step('A');
            subject.AddPrerequisite(new Step('B'));
            Assert.Throws<InvalidOperationException>(() => subject.Complete());
        }

        [Fact]
        public void AddPrerequisiteThrowsWhenCompleted()
        {
            var subject = new Step('A');
            subject.Complete();
            Assert.Throws<InvalidOperationException>(() => subject.AddPrerequisite(new Step('B')));
        }

        [Theory]
        [MemberData(nameof(CanStartCases))]
        public void CanStartReturnsCorrectly(Step subject, bool expected)
        {
            var actual = subject.CanStart();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(DurationCases))]
        public void ConstructorSetsDurationCorrectly(Step subject, int expected)
        {
            var actual = subject.Duration;
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> CanStartCases()
        {
            var step = new Step('A');
            yield return new object[] {step, true};

            step = new Step('B');
            step.AddPrerequisite(new Step('C'));
            yield return new object[] {step, false};

            step = new Step('D');
            var step2 = new Step('E');
            step.AddPrerequisite(step2);
            step2.Complete();
            yield return new object[] {step, true};

            step = new Step('F');
            step.Complete();
            yield return new object[] {step, false};

            step = new Step('G');
            step.Start();
            yield return new object[] {step, false};
        }

        public static IEnumerable<object[]> DurationCases()
        {
            yield return new object[] { new Step('A'), 1};
            yield return new object[] { new Step('Z'), 26};
            yield return new object[] { new Step('A', 60), 61};
            yield return new object[] { new Step('Z', 60), 86};
        }
    }
}
