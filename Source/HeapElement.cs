using System;

namespace Heap
{
    class HeapElement<T> where T : IComparable<T>
    {
        public int Index;
        public T Data;
    }
}
