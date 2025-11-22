using System.Collections.Generic;
using SpaceBattle.Interfaces;

namespace SpaceBattle.Adapters
{
    public class QueueAdapter<T> : IQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>();

        public void Enqueue(T item) => queue.Enqueue(item);
        public T Dequeue() => queue.Dequeue();
        public int Count => queue.Count;
    }
}