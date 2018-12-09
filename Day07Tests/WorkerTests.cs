using Day07;
using Xunit;

namespace Day07Tests
{
    public class WorkerTests
    {
        [Fact]
        public void WorkerFlowOperatesCorrectly()
        {
            var subject = new Worker(1);
            Assert.True(subject.CanAssignWork);
            Assert.False(subject.Completed);
            Assert.Null(subject.CurrentStep);
            var step = new Step('A');
            subject.AssignWork(step);
            Assert.False(subject.CanAssignWork);
            Assert.False(subject.Completed);
            Assert.Equal(step, subject.CurrentStep);
            subject.Tick();
            Assert.False(subject.CanAssignWork);
            Assert.True(subject.Completed);
            Assert.Equal(step, subject.CurrentStep);
            subject.Reset();
            Assert.True(subject.CanAssignWork);
            Assert.False(subject.Completed);
            Assert.Null(subject.CurrentStep);
        }
    }
}
