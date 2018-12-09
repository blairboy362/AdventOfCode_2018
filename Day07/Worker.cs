namespace Day07
{
    public class Worker
    {
        private readonly int _id;
        private int _ticksOnCurrentStep;

        public Worker(int id)
        {
            _id = id;
        }

        public Step CurrentStep { get; private set; }

        public bool CanAssignWork => CurrentStep == null;

        public bool Completed
        {
            get
            {
                if (CurrentStep == null)
                {
                    return false;
                }

                return _ticksOnCurrentStep == CurrentStep.Duration;
            }
        }

        public void AssignWork(Step step)
        {
            CurrentStep = step;
            CurrentStep.Start();
            _ticksOnCurrentStep = 0;
        }

        public void Tick()
        {
            _ticksOnCurrentStep++;
        }

        public void Reset()
        {
            CurrentStep = null;
            _ticksOnCurrentStep = 0;
        }

        public override string ToString()
        {
            return $"Worker {_id}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Worker) obj);
        }

        public override int GetHashCode()
        {
            return _id;
        }

        protected bool Equals(Worker other)
        {
            return _id == other._id;
        }
    }
}
