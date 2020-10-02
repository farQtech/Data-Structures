using System.Collections.Generic;

namespace DataStructSandBox.DataStructures
{         
    /// <summary>
    /// A class representation of a Binary Tree
    /// </summary>
    /// <typeparam name="T">Generic type that must implenment
    /// the ICompareable interface</typeparam>
    class BinaryTree<T> where T : IComparable<T>
    {
        private Node<T> _root;

        public BinaryTree()
        {
            this._root = null;
        }

        /// <summary>
        /// Add a single node to the tree
        /// </summary>
        /// <param name="data">The single data element to add to the tree</param>
        public void AddNode(T data) 
        {
            var newNode = new Node()
            {
                Data = data,
                LeftChild = null,
                RightChild = null
            };

            if(this._root == null) 
            {
                this._root = newNode;
            }
            else 
            {
                AddRecursive(this._root, newNode);
            }
        }

        /// <summary>
        /// Add a collection of nodes to the tree
        /// </summary>
        /// <param name="nodesToAdd">A collection of data to add to the tree</param>
        public void AddNodes(IEnumerable<T> dataToAdd) 
        {
            foreach (var data in dataToAdd)
            {
                AddNode(data);
            }
        }

        /// <summary>
        /// Private method used to recurse the tree and 
        /// determine the correct location for the new node
        /// </summary>
        /// <param name="currentRoot">The currently traversed root</param>
        /// <param name="newNode">The node to add to the tree</param>
        private void AddRecursive(Node<T> currentRoot, Node<T> newNode)
        {
            if(currentRoot == null) 
            {
                currentRoot = newNode;
            }

            // add to the left if less than or equal to the current root
            // add to the right if greater than the current root
            if(Compare(currentRoot.Data, newNode.Data) >= 0) 
            {
                if(currentRoot.LeftChild == null)
                {
                    currentRoot.LeftChild = newNode;
                }
                else 
                {
                    AddRecursive(currentRoot.LeftChild, newNode);
                }
            }
            else
            {
                if(currentRoot.RightChild == null)
                {
                    currentRoot.RightChild = newNode;
                }
                else 
                {
                    AddRecursive(currentRoot.RightChild, newNode);
                }
            }
        }

        /// <summary>
        /// Reset the tree (removes root and all children)
        /// </summary>
        public void Clear() 
        {
            this._root = null;
        }
        
        /// <summary>
        /// Search the tree for some T value contianed in a node
        /// </summary>
        /// <param name="dataToSearch">The data value to search for</param>
        /// <returns>true if found, false otherwise</returns>
        public bool Search(T dataToSearch) 
        {
            // base case - tree is empty
            if(this._root == null)
            {
                return false;
            }

            var upcomingNodes = new Queue<Node<T>>();
            upcomingNodes.Enqueue(this._root);

            // search until we find a match or reach the end of the tree
            while(upcomingNodes.Count > 0) 
            {
                var currentNode = upcomingNodes.Dequeue();

                if(Compare(currentNode.Data, dataToSearch) == 0)
                {
                    return true;
                }

                if(currentNode.LeftChild != null)
                {
                    upcomingNodes.Enqueue(currentNode.LeftChild);
                }

                if(currentNode.RightChild != null)
                {
                    upcomingNodes.Enqueue(currentNode.RightChild);
                }
            }
        
            return false;
        }
    
        /// <summary>
        /// Inner class used to represent the nodes of the tree
        /// </summary>
        /// <typeparam name="T">Generic type that must implenment
        /// the ICompareable interface</typeparam>
        class Node<T> where T : IComparable<T> {
            public T Data { get; set; }    
            public Node<T> LeftChild { get; set; }
            public Node<T> RightChild { get; set; }
        }
    }
}
