namespace BinarySearchTree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class TreeNode<T> : IEnumerable<T> where T : IComparable<T>
    {
        private TreeNode<T> leftChild; 
        private TreeNode<T> rightChild; 

        public TreeNode(T value)
        {
            this.Value = value;
            this.SubTreeCount = 1;
        }

        public T Value { get; set; }

        public TreeNode<T> Parent { get; set; }

        public TreeNode<T> LeftChild
        {
            get
            {
                return this.leftChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.leftChild = value;
            }
        }

        public TreeNode<T> RightChild
        {
            get
            {
                return this.rightChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.rightChild = value;
            }
        }

        public int SubTreeCount { get; set; }

        public bool IsLeftChild
        {
            get
            {
                return this.Parent != null && 
                       this.Parent.HasLeftChild && 
                       this.Parent.LeftChild == this;
            }
        }

        public bool IsRightChild
        {
            get
            {
                return this.Parent != null && 
                       this.Parent.HasRightChild && 
                       this.Parent.RightChild == this;
            }
        }

        public bool HasLeftChild
        {
            get
            {
                return this.LeftChild != null;
            }
        }

        public bool HasRightChild
        {
            get
            {
                return this.RightChild != null;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.HasLeftChild)
            {
                foreach (var node in this.LeftChild)
                {
                    yield return node;
                }
            }

            yield return this.Value;

            if (this.HasRightChild)
            {
                foreach (var node in this.RightChild)
                {
                    yield return node;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}