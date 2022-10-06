// Proof-of-concept XorLinkedList
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace XorLinkedList
{
    

    public unsafe class XorLinkedList : IDisposable, IEnumerable<int>
    {
        private Node* _first = null;
        private Node* _second = null;

        public static void XorTest()
        {
            Node* first = null;
            Node* last = null;
            Node* middle = null;
            try
            {
                first = _allocate(1);
                last = _allocate(2);
                Debug.Assert(_next(first, last) == first);
                Debug.Assert(_prev(first, last) == last);

                middle = _insertMiddle(first, last, 3);
                Debug.Assert(_ptrXor(middle->xorLink, first) == last);
                Debug.Assert(_ptrXor(middle->xorLink, last) == first);

                var addr = _ptrXor(first, last);
                Debug.Assert(_ptrXor(addr, first) == last);
                Debug.Assert(_ptrXor(addr, last) == first);
                Debug.Assert(_ptrXor(first, addr) == last);
                Debug.Assert(_ptrXor(last, addr) == first);

                Debug.Assert(_next(first, middle) == last);
                Debug.Assert(_next(middle, last) == first);
                Debug.Assert(_prev(middle,last) == first);
                Debug.Assert(_prev(first, middle) == last);
            }
            finally
            {
                if(first != null)
                    Marshal.FreeHGlobal((IntPtr) first);
                if (last != null)
                    Marshal.FreeHGlobal((IntPtr)last);
                if (middle != null)
                    Marshal.FreeHGlobal((IntPtr)middle);
            }
        }

        public XorLinkedList(IEnumerable<int> values)
        {
            using (var e = values.GetEnumerator())
            {
                // _
                if (!e.MoveNext())
                    throw new ArgumentException("XorLinkedList needs at least two elements");
                _first = _allocate();
                var firstValue = e.Current;

                // b <-> a
                if (!e.MoveNext())
                    throw new ArgumentException("XorLinkedList needs at least two elements");
                _second = _allocate(firstValue);
                _first->_Value = e.Current;
                // both nodes would have (A xor A) as their link fields, which is just null
                
                // b <-> c <-> d <-> ... <-> z <-> a
                while(e.MoveNext())
                {
                    _first = _insertMiddle(_first, _second, e.Current);
                }

                // a <-> b <-> c <-> ... <-> z
                var newFirst = _second;
                _second = _next(_first, _second);
                _first = newFirst;
            }
        }

        private static Node* _insertMiddle(Node* first, Node* second, int value)
        {
            var node = _allocate(_ptrXor(first, second), value);
            var prev = _prev(first, second);
            first->xorLink = _ptrXor(prev, node);
            var next = _next(first, second);
            second->xorLink = _ptrXor(node, next);
            return node;
        }

        private static Node* _allocate(int value = 0)
        {
            return _allocate(null, value);
        }

        private static Node* _allocate(Node* link, int value = 0)
        {
            var node = (Node*) Marshal.AllocHGlobal(sizeof (Node));
            node->xorLink = link;
            node->_Value = value;
            return node;
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct Node
        {
            internal int _Value;
            internal Node* xorLink;
        }

        private static Node* _ptrXor(Node* a, Node* b)
        {
            return (Node*)((ulong)a ^ (ulong)b);//very fragile
        }

        private static Node* _next(Node* first, Node* second)
        {
            return _ptrXor(second->xorLink, first); 
        }

        private static Node* _prev(Node* first, Node* second)
        {
            return _next(second, first);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                //release managed resources
            }
            //release unmanaged resources
            var first = _first;
            var second = _second;

            var start = first;
            while(true)
            {
                var next = _next(first, second);
                Marshal.FreeHGlobal((IntPtr)first);
                if(next == start)
                    break;
                first = second;
                second = next;
            }
            Marshal.FreeHGlobal((IntPtr)second);
        }

        #region Implementation of IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IEnumerable<out int>

        private class XorEnumerator : IEnumerator<int>, IEnumerable<int>
        {
            private Node* _first;
            private Node* _second;
            private readonly Node* _start;
            private int _index = 0;
            private int _current = 0;

            public XorEnumerator(Node* first, Node* second)
            {
                _first = first;
                _second = second;
                _start = first;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                // nothing to do
            }

            #endregion

            #region Implementation of IEnumerator

            public bool MoveNext()
            {
                switch (_index++)
                {
                    case 0:
                        _current = _first->_Value;
                        return true;
                    case 1:
                        _current = _second->_Value;
                        return true;
                    default:
                        var next = _next(_first, _second);
                        if(next == _start)
                            return false;

                        _current = next->_Value;
                        _first = _second;
                        _second = next;
                        return true;
                }
            }

            public void Reset()
            {
                throw new NotSupportedException("IEnumerator.Reset is not supported.");
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            #endregion

            #region Implementation of IEnumerator<out int>

            public int Current
            {
                get { return _current; }
            }

            #endregion

            #region Implementation of IEnumerable

            public IEnumerator<int> GetEnumerator()
            {
                //return copy
                return new XorEnumerator(_first, _second);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new XorEnumerator(_first, _second);
        }

        public IEnumerable<int> GetReverse()
        {
            var last = _prev(_first, _second);
            return new XorEnumerator(last, _prev(last, _first));
        }

        #endregion
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            XorLinkedList.XorTest();

            var rand = new Random();
            var values = Enumerable.Range(0, 15).Select(i => rand.Next(3*i, 3*i+3));
            using(var xs = new XorLinkedList(values))
            {
                Console.WriteLine("Forward");
                foreach (var x in xs)
                    Console.Write("{0} ", x);
                Console.WriteLine();

                Console.WriteLine("Backward");
                foreach (var x in xs.GetReverse())
                    Console.Write("{0} ", x);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
