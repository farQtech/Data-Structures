using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructSandBox.DataStructures
{

    public interface IStack<T>{

        int Size();

        bool IsEmpty();

        T Pop();

        void Push(T data);

        T Top();


    }

    class Stack<T> : IStack<T>, IEnumerable
    {
       private  LinkedList<T> ll = new LinkedList<T>();

        public Stack()
        {

        }

        public int Size() => this.ll.Count;

        public bool IsEmpty() => this.ll.Count == 0;

        public T Pop()
        {
            if (this.IsEmpty())
                throw new NullReferenceException();
            T data = this.ll.First.Value;
            this.ll.RemoveFirst();
            return data;
        }

        public void Push(T data)
        {
            this.ll.AddFirst(data);
        }

        public T Top()
        {
            return this.ll.First.Value;
        }

        public IEnumerator GetEnumerator() => this.ll.GetEnumerator();
    }

    ////// driver 
    ////public class Program
    ////{
    ////    public static int Main()
    ////    {
    ////        Stack<int> st = new Stack<int>();

    ////        st.Push(1);
    ////        st.Push(8);
    ////        st.Push(6);
    ////        st.Push(7);

    ////       foreach(int i in st)
    ////        {
    ////            Console.Write("{0} ", i);
    ////        }

    ////        // output: 7 6 8 1
    ////        // last value is at the top

    ////        return 0;
    ////    }
    ////}
}
