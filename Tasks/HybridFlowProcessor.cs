using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> storage;

        public HybridFlowProcessor()
        {
            storage = new DoublyLinkedList<T>();
        }

        public T Dequeue()
        {
            if (storage.Length == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            var item = storage.ElementAt(storage.Length - 1);
            storage.RemoveAt(storage.Length - 1);
            return item;
        }

        public void Enqueue(T item)
        {
            storage.AddAt(0, item);
        }

        public T Pop()
        {
            if (storage.Length == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            var item = storage.ElementAt(storage.Length - 1);
            storage.RemoveAt(storage.Length - 1);
            return item;
        }

        public void Push(T item)
        {
            storage.Add(item);
        }
    }
}
