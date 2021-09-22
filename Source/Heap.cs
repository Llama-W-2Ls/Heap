using System;
using System.Collections.Generic;
using System.Linq;

namespace Heap
{
    public class Heap<T> where T : IComparable<T>
    {
        readonly List<HeapElement<T>> Values;

        #region Properties

        public int Count => Values.Count;

        #endregion

        #region Constructors

        public Heap()
        {
            Values = new List<HeapElement<T>>();
        }

        public Heap(int capacity)
        {
            Values = new List<HeapElement<T>>(capacity);
        }

        #endregion

        #region Main Methods

        /// <summary>
        /// Peeks the smallest element in the heap
        /// </summary>
        /// <returns>The first element, without removing it from the heap</returns>
        public T Peek()
        {
            if (Values.Count == 0)
                throw new Exception("No more items in heap to extract");

            return Values[0].Data;
        }

        /// <summary>
        /// Extracts the smallest element in the heap
        /// </summary>
        /// <returns>The first element, and removes it from the heap</returns>
        public T Extract()
        {
            if (Values.Count == 0)
                throw new Exception("No more items in heap to extract");

            var topItem = Peek();
            var lastItem = Values.Last();

            // Move last item to first position
            lastItem.Index = 0;
            Values[0] = lastItem;

            SortDown(lastItem);
            Values.RemoveAt(Values.Count - 1);

            return topItem;
        }

        public void Add(T item)
        {
            var heapItem = new HeapElement<T>()
            {
                Data = item,
                Index = Values.Count
            };
            Values.Add(heapItem);

            SortUp(heapItem);
        }

        public void AddRange(T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Add(items[i]);
            }
        }

        public T[] ToArray()
        {
            var copy = new T[Values.Count];

            for (int i = 0; i < Values.Count; i++)
            {
                copy[i] = Values[i].Data;
            }

            return copy;
        }

        public void Clear()
        {
            Values.Clear();
        }

        #endregion

        #region Sorting Methods

        void SortUp(HeapElement<T> item)
        {
            var parent = GetParent(item);

            if (parent.Data.CompareTo(item.Data) > 0)
            {
                Swap(item, parent);
                SortUp(item);
            }
        }

        void SortDown(HeapElement<T> item)
        {
            var leftChild = GetLeftChild(item);
            var rightChild = GetRightChild(item);

            var smallestChild = leftChild.Data.CompareTo(rightChild.Data) > 0 ? rightChild : leftChild;

            if (item.Data.CompareTo(smallestChild.Data) > 0)
            {
                Swap(item, smallestChild);
                SortDown(item);
            }
        }

        void Swap(HeapElement<T> item1, HeapElement<T> item2)
        {
            // Swap positions in array
            var item1Temp = Values[item1.Index];
            Values[item1.Index] = Values[item2.Index];
            Values[item2.Index] = item1Temp;

            // Swap virtual indexes
            var item1Index = item1.Index;
            item1.Index = item2.Index;
            item2.Index = item1Index;
        }

        #endregion

        #region Tree Traversal Methods

        HeapElement<T> GetParent(HeapElement<T> item)
        {
            // If master node, there are no parents
            if (item.Index == 0)
                return Values[0];

            var index = (item.Index + 1) / 2;
            return Values[index - 1];
        }

        HeapElement<T> GetLeftChild(HeapElement<T> item)
        {
            var index = (item.Index + 1) * 2;

            if (index >= Count)
                return Values[item.Index];

            return Values[index - 1];
        }

        HeapElement<T> GetRightChild(HeapElement<T> item)
        {
            var index = (item.Index + 1) * 2 + 1;

            if (index >= Count)
                return Values[item.Index];

            return Values[index - 1];
        }

        #endregion
    }
}
