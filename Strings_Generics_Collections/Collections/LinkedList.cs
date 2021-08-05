using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    public class LinkedList<T> : ICollection<T>
    {
        private Node<T> head;

        private Node<T> current;

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var newNode = new Node<T> { Value = item };
            if (head == null)
            {
                head = newNode;
                current = head;
            }
            else
            {
                current.Next = newNode;
                current = newNode;
            }

            Count++;
        }

        public bool Remove(T item)
        {
            Node<T> lastNode = null;
            var currentNext = head;
            do
            {
                if (currentNext.Value.Equals(item))
                {
                    if (head == currentNext)
                    {
                        head = null;
                        Count--;
                        return true;
                    }

                    if (lastNode != null)
                    {
                        lastNode.Next = currentNext.Next;
                    }

                    currentNext = null;
                    Count--;
                    return true;
                }

                lastNode = currentNext;
                currentNext = currentNext.Next;
            } while (currentNext != null);

            return false;
        }

        public void Clear()
        {
            head = null;
            current = null;
        }
       
        public IEnumerator<T> GetEnumerator()
        {
            return new NodeEnumerator<T>(head);
        }
      
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(T item)
        {
            return Enumerable.Contains(this, item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex || array.Length < Count)
            {
                return;
            }

            var index = arrayIndex;
            foreach (var item in this)
            {
                array[index] = item;
                index++;
            }
        }
    }
}
