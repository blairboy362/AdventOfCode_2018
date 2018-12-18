using System.Collections;
using System.Collections.Generic;

namespace Day12
{
    public class RollingBuffer<T> : ICollection<T>
    {
        private readonly int _capacity;
        private readonly Queue<T> _queue;

        public RollingBuffer(int capacity)
        {
            _capacity = capacity;
            _queue = new Queue<T>(_capacity);
            for (var i = 0; i < _capacity; i++)
            {
                _queue.Enqueue(default(T));
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _queue.Dequeue();
            _queue.Enqueue(item);
        }

        public void Clear()
        {
            _queue.Clear();
            for (var i = 0; i < _capacity; i++)
            {
                _queue.Enqueue(default(T));
            }
        }

        public bool Contains(T item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public int Count => _queue.Count;

        public bool IsReadOnly { get; }
    }
}
