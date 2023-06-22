using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Length { get; private set; }

        public void Add(T e)
        {
            var newNode = new Node<T>(e);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }

            Length++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            if (index == Length)
            {
                Add(e);
                return;
            }

            var newNode = new Node<T>(e);

            if (index == 0)
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            else
            {
                var currentNode = GetNodeAt(index);
                var previousNode = currentNode.Previous;

                previousNode.Next = newNode;
                newNode.Previous = previousNode;

                newNode.Next = currentNode;
                currentNode.Previous = newNode;
            }

            Length++;
        }

        public T ElementAt(int index)
        {
            var node = GetNodeAt(index);
            return node.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        public void Remove(T item)
        {
            var currentNode = head;

            while (currentNode != null)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Value, item))
                {
                    RemoveNode(currentNode);
                    return;
                }

                currentNode = currentNode.Next;
            }
        }

        public T RemoveAt(int index)
        {
            var nodeToRemove = GetNodeAt(index);
            RemoveNode(nodeToRemove);
            return nodeToRemove.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void RemoveNode(Node<T> node)
        {
            if (node.Previous == null)
            {
                head = node.Next;
            }
            else
            {
                node.Previous.Next = node.Next;
            }

            if (node.Next == null)
            {
                tail = node.Previous;
            }
            else
            {
                node.Next.Previous = node.Previous;
            }

            Length--;
        }

        private Node<T> GetNodeAt(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            Node<T> currentNode;
            if (index <= Length / 2)
            {
                currentNode = head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
            }
            else
            {
                currentNode = tail;
                for (int i = Length - 1; i > index; i--)
                {
                    currentNode = currentNode.Previous;
                }
            }

            return currentNode;
        }
    }

    internal class Node<T>
    {
        public T Value { get; }
        public Node<T> Previous { get; internal set; }
        public Node<T> Next { get; internal set; }

        public Node(T value)
        {
            Value = value;
        }
    }
}

