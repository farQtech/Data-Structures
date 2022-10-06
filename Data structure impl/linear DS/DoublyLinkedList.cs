using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace DataStructSandBox.DataStructures
{

    public interface IDoublyLinkedList<T>{

        void Clear();

        int Size();

        bool IsEmpty();

        void Add(T elem);

        void AddAtHead(T elem);

        void Append(T elem);

        T Peek();

        T RemoveAt(int idx);

        T RemoveLast();

        T RemoveFirst();

        T PeekLast();

        T Remove(T data);

        T IndexOf(T data);
        
        bool Contains(T data);
 }

    class DoublyLinkedList<T> : IDoublyLinkedList
    {
        int Length = 0;
        private Node<T> Head;
        private Node<T> Tail;

        class Node<T>
        {
            public T data;
            public Node<T> next;
            public Node<T> prev;

            public Node(T data, Node<T> prev = null, Node<T> next = null)
            {
                this.data = data;
                this.next = next;
                this.prev = prev;
            }
        }

        // empty LL O(n)
        public void Clear()
        {
            Node<T> trav = this.Head;
            while (trav != null)
            {
                Node<T> next = trav.next;
                trav.prev = trav.next = null;// resetting both pre as well as next pointer to null
                trav.data = default(T); // resetting data
                trav = next; // move ahead
            }

            this.Head = this.Tail = trav = null; // reset head, tail and trav
            this.Length = 0; // reset size of ll
        }

        public int Size() => this.Length;

        public bool IsEmpty() => this.Length == 0;

        public void Add(T elem) => Append(elem);

        public void AddAtHead(T elem)
        {
            if (this.IsEmpty())
                this.Head = this.Tail = new Node<T>(elem);
            else
            {
                this.Head.prev = new Node<T>(elem);
                this.Head = this.Head.prev;
            }

            this.Length++;
        }

        public void Append(T elem)
        {
            if (this.IsEmpty())
                this.Head = this.Tail = new Node<T>(elem);
            else
            {
                this.Tail.next = new Node<T>(elem);
                this.Tail = this.Tail.next;
            }

            this.Length++;
        }

        // check val of first node if non empty
        public T Peek()
        {
            if (this.IsEmpty())
                throw new NullReferenceException();
            return this.Head.data;
        }

        public T PeekLast()
        {
            if (this.IsEmpty())
                throw new NullReferenceException();
            return this.Tail.data;
        }

        public T RemoveFirst()
        {
            if (this.IsEmpty())
                throw new NullReferenceException();
            T data = this.Head.data;
            this.Head = Head.next;

            this.Length--;

            if (this.IsEmpty()) this.Tail = null;// if list is empty then set tail to null as well

            else this.Head.prev = null; // free memory for node which is now deleted

            return data;
        }

        public T RemoveLast()  
        {
            if (this.IsEmpty())
                throw new NullReferenceException();
            T data = this.Tail.data;
            this.Tail = Tail.prev;

            this.Length--;

            if (this.IsEmpty()) this.Head = null;// if list is empty then set head to null as well

            else this.Tail.next = null; // free memory for node which is now deleted

            return data;
        }

        private T RemoveNode(Node<T> node)  
        {
            // If the node is the head node
            if (node.prev == null) return this.RemoveFirst();
            // If the node is tail
            if (node.next == null) return this.RemoveLast();

            // skip this node/ adjust pointers
            node.prev.next = node.next;
            node.next.prev = node.prev;

            T data = node.data;// save data to return

            node.data = default(T);// reset data
            node = node.prev = node.next = null; // reset pointers

            this.Length--;
            return data;
        }

        public T RemoveAt(int idx) 
        {
            if (idx < 0 || idx >= this.Length) throw new IndexOutOfRangeException();

            Node<T> trav;

            if(idx < this.Length / 2)
            {
                trav = this.Head;
                for (int i = 0; i != idx; i++)
                    trav = trav.next;
            }
            else
            {
                trav = this.Tail;
                for (int i = this.Length-1; i != idx; i--)
                    trav = trav.prev;
            }

           return this.RemoveNode(trav);
        }

        public T Remove(T data)
        {
            throw new NotImplementedException();
        }

        public T IndexOf(T data)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T data)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            if (this.IsEmpty())
                return "[]";
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");

            Node<T> trav = this.Head;
            while (trav.next != null)
            {
                sb.AppendFormat("{0}, ", trav.data);
                trav = trav.next;
            }
            sb.AppendFormat("{0} ]", trav.data);
            return sb.ToString();
        }

    }

    // driver code

    ////public class Program
    ////{
    ////    public static int Main()
    ////    {
    ////        DoublyLinkedList<int> DLL = new DoublyLinkedList<int>();

    ////        DLL.Add(1);
    ////        DLL.Add(5);
    ////        DLL.Add(57);
    ////        DLL.Add(9);
    ////        DLL.Add(23);
    ////        DLL.Add(74);

    ////        Console.WriteLine(DLL.ToString());

    ////        // output: [ 1, 5, 57, 9, 23, 74 ]

    ////        return 0;
    ////    }
    ////}
}
