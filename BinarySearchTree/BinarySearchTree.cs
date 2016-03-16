namespace BinarySearchTree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class BinarySearchTree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private TreeNode<T> root;

        public int Count { get; private set; }

        bool ICollection<T>.IsReadOnly { get; }

        public T this[int index]
        {
            get
            {
                return this.GetItemAt(index, this.root).Value;
            }
        }

        private TreeNode<T> GetItemAt(int index, TreeNode<T> node)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Index is outside of the tree");
            }

            while (true)
            {
                var leftSubTreeCount = 0;
                if (node.HasLeftChild)
                {
                    leftSubTreeCount += node.LeftChild.SubTreeCount;
                }

                if (leftSubTreeCount == index)
                {
                    return node;
                }

                if (leftSubTreeCount > index)
                {
                    node = node.LeftChild;
                    continue;
                }

                if (leftSubTreeCount <= index)
                {
                    index -= leftSubTreeCount + 1;
                    node = node.RightChild;
                }
            }
        }

        public IEnumerable<T> Range(T min, T max)
        {
            var range = new Collection<T>();
            if (this.root != null)
            {
                this.GetElementsInRange(min, max, this.root, range);
            }

            return range;
        }

        private void GetElementsInRange(T min, T max, TreeNode<T> node, Collection<T> items)
        {
            if (node.HasLeftChild)
            {
                if (node.Value.CompareTo(min) > 0)
                {
                    this.GetElementsInRange(min, max, node.LeftChild, items);
                }
            }

            if (node.Value.CompareTo(min) >= 0 &&
                node.Value.CompareTo(max) <= 0)
            {
                items.Add(node.Value);
            }

            if (node.HasRightChild)
            {
                if (node.Value.CompareTo(max) < 0)
                {
                    this.GetElementsInRange(min, max, node.RightChild, items);
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in this.root)
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            var newNode = new TreeNode<T>(item);
            var currentNode = this.root;

            while (true)
            {
                if (currentNode == null)
                {
                    this.root = newNode;
                    break;
                }

                if (currentNode.Value.CompareTo(item) > 0)
                {
                    if (!currentNode.HasLeftChild)
                    {
                        currentNode.LeftChild = newNode;
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Value.CompareTo(item) < 0)
                {
                    if (!currentNode.HasRightChild)
                    {
                        currentNode.RightChild = newNode;
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                else
                {
                    return;
                }
            }

            this.IncreaseParentsCount(newNode);
            this.Count++;
        }

        public void Clear()
        {
            this.Count = 0;
            this.root = null;
        }

        public bool Contains(T item)
        {
            var currentNode = this.root;

            while (true)
            {
                if (currentNode == null)
                {
                    break;
                }

                if (currentNode.Value.CompareTo(item) > 0)
                {
                    if (!currentNode.HasLeftChild)
                    {
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Value.CompareTo(item) < 0)
                {
                    if (!currentNode.HasRightChild)
                    {
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            var nodeToDelete = this.root;

            while (true)
            {
                if (nodeToDelete == null)
                {
                    return false;
                }

                if (nodeToDelete.Value.CompareTo(item) > 0)
                {
                    if (!nodeToDelete.HasLeftChild)
                    {
                        return false;
                    }

                    nodeToDelete = nodeToDelete.LeftChild;
                }
                else if (nodeToDelete.Value.CompareTo(item) < 0)
                {
                    if (!nodeToDelete.HasRightChild)
                    {
                        return false;
                    }

                    nodeToDelete = nodeToDelete.RightChild;
                }
                else
                {
                    if (nodeToDelete.Parent == null)
                    {
                        this.root = null;
                        break;
                    }

                    if (nodeToDelete.HasLeftChild && 
                        nodeToDelete.HasRightChild)
                    {
                        // Find the smallest value in the node's right subtree and copy its value to the current node. Then we'll remove the new smallest node from the tree.
                        // NOTE: The new node to delete CANNOT have 2 children, so the cases bellow WILL handle the deletion.
                        var nextNode = this.GetItemAt(0, nodeToDelete.RightChild);
                        nodeToDelete.Value = nextNode.Value;
                        nodeToDelete = nextNode;
                    }

                    if (nodeToDelete.HasLeftChild)
                    {
                        if (nodeToDelete.IsLeftChild)
                        {
                            nodeToDelete.Parent.LeftChild = nodeToDelete.LeftChild;
                            break;
                        }
                        
                        nodeToDelete.Parent.RightChild = nodeToDelete.LeftChild;
                        break;
                    }

                    if (nodeToDelete.HasRightChild)
                    {
                        if (nodeToDelete.IsLeftChild)
                        {
                            nodeToDelete.Parent.LeftChild = nodeToDelete.RightChild;
                            break;
                        }
                        
                        nodeToDelete.Parent.RightChild = nodeToDelete.RightChild;
                        break;
                    }

                    // Node has no children
                    if (nodeToDelete.IsLeftChild)
                    {
                        nodeToDelete.Parent.LeftChild = null;
                        break;
                    }

                    nodeToDelete.Parent.RightChild = null;
                    break;
                }
            }

            this.DecreaseParentsCount(nodeToDelete);
            this.Count--;
            return true;
        }

        private void IncreaseParentsCount(TreeNode<T> node)
        {
            var currentNode = node.Parent;
            while (currentNode != null)
            {
                currentNode.SubTreeCount++;
                currentNode = currentNode.Parent;
            }
        }

        private void DecreaseParentsCount(TreeNode<T> node)
        {
            var currentNode = node.Parent;
            while (currentNode != null)
            {
                currentNode.SubTreeCount--;
                currentNode = currentNode.Parent;
            }
        }
    }
}
