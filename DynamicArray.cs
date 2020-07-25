using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataStructSandBox.DataStructures
{

    public interface IDynamicArray<T>{
        void Add(T elem);

        int Length();

        int GetFirstIndexOf(T elem);

        int GetLastIndexOf(T elem);

        T GetElemAt(int idx);

        bool IsEmpty();

        string ToString();
    }

    public class DynamicArray<T> : IDynamicArray<T>, IEnumerable<T>
    {
        /**
         * 
         * In C# all arrays are dynamic but I'll implement an dynamic array using System.Array class anyway
         * I know it doesn't make any sense to use a dynamic array as static array to implement a dynamic array
         * but I am just doing it for practice (:
         * 
         * So the idea is,
         * 1- use an internal array of fixed size.
         * 2- add elements to it
         * 3- if array is about to overflow, create new array of double size than before and copy all elements to
         *    the new array, add next element to newly created array.
         * 
         */

        // since we are using Templates, default will be null for ref. type and 0 for numeric
       private T[] _arr;
       private int ItemsInArray = 0; // actual filled slots

        public DynamicArray()
        {
            this._arr = new T[15]; // initial capacity of the array.
        }

        public void Add(T elem)
        {
            if (this.ItemsInArray + 1 >= this._arr.Length)
            {
                T[] temp = new T[this._arr.Length * 2];
                Array.ConstrainedCopy(this._arr, 0, temp, 0, this._arr.Length);
                this._arr = temp;
            }
            this._arr[this.ItemsInArray] = elem;
            this.ItemsInArray++;
        }

        public T GetElemAt(int idx)
        {
            if(idx <= this._arr.Length)
                return this._arr[idx];
            throw new IndexOutOfRangeException();
        }

        public IEnumerator<T> GetEnumerator() => this._arr.Take(this._arr.Length).GetEnumerator();

        public int GetFirstIndexOf(T elem)
        {
            throw new NotImplementedException();
        }

        public int GetLastIndexOf(T elem)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty() => this._arr.Length == 0;

        public int Length() => this._arr.Length;

        IEnumerator IEnumerable.GetEnumerator() => this._arr.GetEnumerator();

        public override string ToString()
        {
            if (this.ItemsInArray == 0)
                return "[]";
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            for (int i = 0; i < this.ItemsInArray-1; i++)
                sb.AppendFormat("{0}, ",this._arr[i]);
            sb.AppendFormat("{0} ]", this._arr[this.ItemsInArray-1]);
            return sb.ToString();
        }
    }


    // driver
    public class program_
    {
        public static int Main()
        {
            DynamicArray<int> intArr = new DynamicArray<int>();

            intArr.Add(1);
            intArr.Add(5);
            intArr.Add(6);
            intArr.Add(8);
            intArr.Add(7);

            Console.WriteLine(intArr.ToString());
            // output: [ 1, 5, 6, 8, 7 ]

            return 0;
        }
    }
}
