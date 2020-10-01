using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructSandBox.DataStructures
{

    public interface IQueue<T>{

        bool IsEmpty();

        int Size();

        T peeek();

        void Enqueue(T data);

        T Dequeue();

    }

    class Queue<T>: IQueue<T>, IEnumerable
    {
        private LinkedList<T> ll = new LinkedList<T>();

        public bool IsEmpty() => this.ll.Count == 0;

        public int Size() => this.ll.Count;

        public T peeek()
        {
            if (this.IsEmpty())
                throw new NullReferenceException();
            return this.ll.Last.Value;
        }

        public void Enqueue(T data)
        {
            this.ll.AddFirst(data);
        }

        public T Dequeue()
        {
            if (this.IsEmpty())
                throw new NullReferenceException();

           T data =  this.ll.Last.Value;
            this.ll.RemoveLast();
            return data;
        }

        public IEnumerator GetEnumerator() => this.ll.GetEnumerator();
    }

    ////// driver 
    ////public class Program
    ////{
    ////    public static int Main()
    ////    {
    ////        Queue<int> q = new Queue<int>();

    ////        q.Enqueue(1);
    ////        q.Enqueue(9);
    ////        q.Enqueue(96);
    ////        q.Enqueue(5);
    ////        q.Enqueue(75);

    ////        foreach (int i in q)
    ////            Console.Write("{0} ", i);
    ////        // output : 75 5 96 9 1

    ////        return 0;
    ////    }
    ////}
}

