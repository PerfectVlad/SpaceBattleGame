using System.Collections.Generic;

namespace SpaceBattle.Interfaces
{
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        int Count { get; }
    }
}