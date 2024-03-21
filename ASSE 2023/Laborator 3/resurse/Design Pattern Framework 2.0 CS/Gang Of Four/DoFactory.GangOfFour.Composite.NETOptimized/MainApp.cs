using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Composite.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Composite Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Build a tree of shapes
            TreeNode<Shape> root = new TreeNode<Shape>(new Shape("Picture"));
            root.Add(new Shape("Red Line"));
            root.Add(new Shape("Blue Circle"));
            root.Add(new Shape("Green Box"));

            TreeNode<Shape> branch = root.Add(new Shape("Two Circles"));
            branch.Add(new Shape("Black Circle"));
            branch.Add(new Shape("White Circle"));

            // Add and remove a shape
            Shape shape = new Shape("Yellow Line");
            root.Add(shape);
            root.Remove(shape);
            
            // Display tree
            root.Display(1);

            Console.Read();
        }
    }

    // Generic tree node class
    class TreeNode<T> where T : IComparable<T>
    {
        private T node;
        private List<TreeNode<T>> children = new List<TreeNode<T>>();

        // Constructor
        public TreeNode(T node)
        {
            this.node = node;
        }

        // Add a child tree node
        public TreeNode<T> Add(T child)
        {
            TreeNode<T> newNode = new TreeNode<T>(child);
            children.Add(newNode);
            return newNode;
        }

        // Remove a child tree node
        public void Remove(T child)
        {
            foreach (TreeNode<T> treeNode in children)
            {
                if (treeNode.Node.CompareTo(child) == 0)
                {
                    children.Remove(treeNode);
                    return;
                }
            }
        }

        // Readonly node property
        public T Node
        {
            get { return node; }
        }

        // Readonly children property
        public List<TreeNode<T>> Children
        {
            get { return children; }
        }

        // Display node and children at given indentation
        public void Display(int indentation)
        {
            string line = new String('-', indentation) + "+ ";
            Console.WriteLine( line + Node);

            foreach (TreeNode<T> treeNode in children)
            {
                int indent = indentation + 1;
                if (treeNode.Children.Count > 0)
                {
                    treeNode.Display(++indent);
                }
                else
                {
                    line = new String('-', indent) + " ";
                    Console.WriteLine( line + treeNode.Node);
                }
            }
        }
    }

    // Shape class implements generic IComparable interface
    class Shape : IComparable<Shape>
    {
        private string name;

        // Constructor
        public Shape(string name)
        {
            this.name = name;
        }

        // Override ToString method
        public override string ToString()
        {
            return name;
        }

        // IComparable<Shape> Member
        public int CompareTo(Shape other)
        {
            if (this == other)
                return 0;
            else
                return -1;
        }
    }
}
