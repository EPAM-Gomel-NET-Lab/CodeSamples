using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public class NodeEnumerator<T> : IEnumerator<T>
    {
        private readonly Node<T> head;

        private Node<T> current;

        public NodeEnumerator(Node<T> head)
        {
            this.head = head ?? throw new ArgumentNullException(nameof(head));
        }

        public T Current => current.Value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (current == null)
            {
                current = head;
                return true;
            }

            if (current.Next != null)
            {
                current = current.Next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            current = head;
        }
    }
}
