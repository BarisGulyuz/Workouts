using System.Collections;

namespace Workouts.DataStructures.LinkedList
{
    /// <summary>
    /// singly
    /// </summary>
    public class MyLinkedList<T> : IEnumerable<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }

        public void AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);

            if (Head is null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                node.Next = Head;
                Head = node;
            }
        }
        public void RemoveFist()
        {
            if (Head is not null)
            {
                Head = Head.Next;
            }
        }
        public void AddLast(T data)
        {
            Node<T> node = new Node<T>(data);

            if (Head is null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Tail.Next = node;
                Tail = node;
            }
        }
        public void RemoveLast()
        {
            if (Head is null) return;
            if (Head == Tail) Head = Tail = null; return;

            Node<T> current = Head;

            while(current.Next != Tail)
            {
                current = current.Next;
            }

            Tail = current;
            current.Next = null;
        }
        public void AddMiddle(T data)
        {
            if (Head is null)
            {
                AddFirst(data);
            }
            else
            {
                Node<T> currentHead = Head;
                int middle = this.Count() / 2;

                int counter = 0;
                while (currentHead is not null)
                {
                    if (counter == middle - 1)
                    {
                        Node<T> node = new Node<T>(data) { Next = currentHead.Next };
                        currentHead.Next = node;
                        return;
                    }

                    currentHead = currentHead.Next;
                    counter++;
                }
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentHead = Head;

            while (currentHead is not null)
            {
                yield return currentHead.Value;
                currentHead = currentHead.Next;
            }


        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public class Node<T>
    {
        public T Value { get; set; }

        public Node<T> Next { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }
}
