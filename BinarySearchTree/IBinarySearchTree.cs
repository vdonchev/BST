namespace BinarySearchTree
{
    using System.Collections.Generic;

    public interface IBinarySearchTree<T> : ICollection<T>
    {
        IEnumerable<T> Range(T min, T max);
    }
}