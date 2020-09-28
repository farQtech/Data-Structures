using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DataStructSandBox.DataStructures
{
    public interface IPriorityQueue<T>{

        bool Less(int i, int j);

        void Add(T elem);

        bool Contains(T elem);

        T Peek();

        int Size();

        T RemoveAt(int idx);

        T Remove(T elem);

        void Clear();
        
        bool IsEmpty();

    }

    /*
     * Heap only allows types which can be compaired to maintain the 
     * heap invarient.
     */
    class PriorityQueue<T> where T : IComparable<T>, IPriorityQueue<T>
    {
        // number of elements inside heap
        private int heapSize = 0;

        private int heapCapacity = 0; // internal capacity of heap

        private List<T> heap = null; // array to represent heap.

        private Dictionary<T, SortedSet<int>> map = new Dictionary<T, SortedSet<int>>();

        public PriorityQueue()
        {
        }

        public PriorityQueue(int size)
        {
            this.heap = new List<T>(size);
        }

        // heapify in O(n)
        public PriorityQueue(T[] elems)
        {
            this.heapSize = elems.Length;
            this.heap = new List<T>(this.heapSize);

            // place alll elements inside heap
            for(int i = 0; i< this.heapSize; i++)
            {
                if (this.map.ContainsKey(elems[i]))
                    map[elems[i]].Add(i);
                else
                    map[elems[i]] = new SortedSet<int>() { i };

                this.heap.Add(elems[i]);
            }

            // heapify process O(n)
            // start from middle and sink all elems
            for (int i = Math.Max(0, (this.heapSize / 2) - 1); i >= 0; i--)
                this.Sink(i);
        }

        // O(nlog(n))
        public PriorityQueue(Collection<T> elems)
        {
            this.heapSize = elems.Count;
            this.heap = new List<T>(this.heapSize);

            foreach (T i in elems) this.Add(i);
        }

        public bool IsEmpty() => this.heapSize == 0;

        public void Clear()
        {
            this.heapSize = 0;
            this.map.Clear();
            this.heap.Clear();
        }

        public int Size() => this.heapSize;

        public T Peek()
        {
            if (this.IsEmpty()) return default(T);

            return this.heap.ElementAt(0);
        }

        public T Poll() => this.RemoveAt(0);

        // O(1)
        public bool Contains(T elem)
        {
            if (elem.Equals(default(T))) return false;

            return this.map.ContainsKey(elem);
        }

        public void Add(T elem)  
        {
            if (this.heapSize > this.heapCapacity)
                throw new OverflowException();
            this.heap.Add(elem);
            this.heapCapacity++;
            this.heapSize++;

            if (this.map.ContainsKey(elem))
                this.map[elem].Add(this.heapSize);
            else
                this.map[elem] = new SortedSet<int>() { this.heapSize };

            this.Swim(this.heapSize);
        }

        // returns true if elem at idx i is less than elem at idx j
        public bool Less(int i, int j) 
        {
            T node1 = this.heap.ElementAt(i);
            T node2 = this.heap.ElementAt(j);

            return node1.CompareTo(node2) <= 0;
        }


        // O(log(n))
        private void Swim(int k) 
        {
            // get parent of node k

            int parentIdx = (k - 1) / 2;

            // bubble up untill reached root and less than parent
            while(k > 0 && this.Less(k, parentIdx))
            {
                this.Swap(parentIdx, k);
                k = parentIdx;

                parentIdx = (k - 1) / 2;
            }

        }

        // O(log(n))
        private void Sink(int k) 
        {
            while (true)
            {
                int left = 2 * k + 1;
                int right = 2 * k + 2;

                int smallest = left; // assuming left is the smallest of two

                // check if the right one is smallest
                if (right < this.heapSize && this.Less(right, left))
                    smallest = right;

                if (left >= this.heapSize || this.Less(k, smallest)) break;
                this.Swap(smallest, k);

                k = smallest;
            }
        }

        private void Swap(int i, int j)
        {
            T iElem = this.heap.ElementAt(i);
            T jElem = this.heap.ElementAt(j);

            this.heap[i] = jElem;
            this.heap[j] = iElem;

            this.MapSwap(iElem, jElem, i, j);
        }

        private void MapSwap(T iElem, T jElem, int i, int j)
        {
            this.map[iElem].Remove(i);
            this.map[jElem].Remove(j);
            this.map[iElem].Add(j);
            this.map[jElem].Add(i);
        }

        public T Remove(T elem)
        {
            throw new NotImplementedException();
        }

        public T RemoveAt(int idx)
        {
            throw new NotImplementedException();
        }

        private bool IsMinHeap()
        {
            throw new NotImplementedException();
        }

    }
}
