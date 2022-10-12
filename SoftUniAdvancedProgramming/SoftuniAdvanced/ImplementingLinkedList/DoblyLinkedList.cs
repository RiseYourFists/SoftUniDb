using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImplementingLinkedList
{
    public class DoblyLinkedList
    {
        public class Node
        {
            public int Value { get; set; }

            public Node Next { get; set; }

            public Node Previous { get; set; }

            public Node(int value)
            {
                Value = value;
            }
        }

        private Node Head { get; set; }
        private Node Tail { get; set; }
        public int Count { get; set; }

        public void AddFirst(int value)
        {
            
            if (!IsFirst(value))
            {
                var newHead = new Node(value);
                newHead.Next = Head;
                Head.Previous = newHead;
                Head = newHead;
            }
            Count++;
        }

        public void AddLast(int value)
        {
            if (!IsFirst(value))
            {
                var newTail = new Node(value);
                newTail.Previous = Tail;
                Tail.Next = newTail;
                Tail = newTail;
            }
            Count++;
        }

        private bool IsFirst(int value)
        {
            if (Head == null)
            {
                Head = Tail = new Node(value);
                return true;
            }

            return false;
        }

        public int RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Empty list!");
            }

            var firstElement = Head.Value;
            Head = Head.Next;

            if (Head != null)
            {
                Head.Previous = null;
            }
            else
            {
                Tail = null;
            }
            Count--;
            return firstElement;
        }

        public int RemoveLast()
        {
            if(Count == 0)
            {
                throw new InvalidOperationException("Empty list!");
            }

            var lastElement = Head.Value;
            Tail = Tail.Previous;
            if (Tail != null)
            {
                Tail.Next = null;
            }
            else
            {
                Head = null;
            }
            Count--;
            return lastElement;
        }

        public void ForEach(Action<Node> action)
        {
            var currNode = Head;
            while (currNode != null)
            {
                action(currNode);
                currNode = currNode.Next;
            }
        }

        public Node[] ToArray()
        {
            Node[] array = new Node[Count];
            int counter = 0;
            var currNode = Head;
            while (currNode != null)
            {
                array[counter] = currNode;
                counter++;
                currNode = currNode.Next;
            }

            return array;
        }
    }
}
