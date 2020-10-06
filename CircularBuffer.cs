using System;
using System.Collections.Generic;
using Linq;

namespace DataStructSandBox.DataStructures
{
    public interface ICircularBuffer<T>
    {
        T Read();
        
        void Write(T value);

        void Overwrite(T value);

        void Clear();

        void DequeueHead();
    }
    public class CircularBuffer<T>
    {
        private readonly int _capacity;
        private List<T> items;

        public CircularBuffer(int capacity)
        {
            this._capacity = capacity;
            items = new List<T>(capacity);
        }

        public T Read()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("Cannot read from empty buffer");
            }

            var value = items[0];

            DequeueHead();

            return value;
        }

        public void Write(T value)
        {
            if (items.Count == _capacity)
            {
                throw new InvalidOperationException("Cannot write to full buffer");
            }

            items.Add(value);
        }

        public void Overwrite(T value)
        {
            if (items.Count == _capacity)
            {
                DequeueHead();
            }

            Write(value);
        }

        public void Clear() => items.Clear();

        private void DequeueHead() => items = items.Skip(1).ToList();
    }
}
