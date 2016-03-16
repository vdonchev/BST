namespace BinarySearchTreeDemos
{
    using System;
    using System.Collections.Generic;
    using BinarySearchTree;

    public static class BinarySearchTreeDemos
    {
        public static void Main()
        {
            var list = new List<int>() { 1, 2, 3 };

            var bst = new BinarySearchTree<int>();
            bst.Add(5);
            bst.Add(3);
            bst.Add(10);
            bst.Add(4);
            bst.Add(-4);

            foreach (var node in bst)
            {
                Console.WriteLine(node);
            }

            Console.WriteLine($"BST Contains -4: {bst.Contains(-4)}");
            Console.WriteLine($"BST Contains -40: {bst.Contains(-40)}");

            Console.WriteLine(bst.Count);
            bst.Remove(-4);
            Console.WriteLine(bst.Count);

            Console.WriteLine("---------------");
            foreach (var node in bst)
            {
                Console.WriteLine(node);
            }

            var a = bst[3];

            Console.WriteLine("-----");
            bst.Add(-100);
            for (int i = 0; i < bst.Count; i++)
            {
                Console.WriteLine($"Index={i}: {bst[i]}");
            }

            bst.Clear();

            bst.Add(20);
            bst.Add(40);
            bst.Add(10);
            bst.Add(5);
            bst.Add(15);
            bst.Add(30);
            bst.Add(50);
            bst.Add(35);

            foreach (var node in bst)
            {
                Console.WriteLine(node);
            }

            Console.WriteLine(bst[6]);
            bst.Remove(40);
            Console.WriteLine("--------");

            foreach (var node in bst)
            {
                Console.WriteLine(node);
            }

            Console.WriteLine("In ragne [10..30]:");
            var range = bst.Range(10, 30);
            foreach (var item in range)
            {
                Console.WriteLine(item);
            }
        }
    }
}
