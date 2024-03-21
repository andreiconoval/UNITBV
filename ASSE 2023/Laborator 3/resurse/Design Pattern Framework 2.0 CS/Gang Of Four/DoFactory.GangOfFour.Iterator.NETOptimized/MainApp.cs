using System;
using System.Collections;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Iterator.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Iterator Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create and populate a type-safe collection
            ItemCollection<Item> collection = 
                new ItemCollection<Item>();

            collection.Add(new Item("Item 0"));
            collection.Add(new Item("Item 1"));
            collection.Add(new Item("Item 2"));
            collection.Add(new Item("Item 3"));
            collection.Add(new Item("Item 4"));
            collection.Add(new Item("Item 5"));
            collection.Add(new Item("Item 6"));
            collection.Add(new Item("Item 7"));
            collection.Add(new Item("Item 8"));

            Console.WriteLine("Iterate front to back");
            foreach (Item item in collection)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("\nIterate back to front");
            foreach (Item item in collection.BackToFront)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();

            // Iterate given range and step over even ones
            Console.WriteLine("\nIterate range (1-7) in steps of 2");
            foreach (Item item in collection.FromToStep(1, 7, 2))
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();

            // Wait for user
            Console.Read();
        }
    }

    // "ConcreteAggregate" 

    class ItemCollection<T> : IEnumerable<T>
    {
        private List<T> items = new List<T>();

        public void Add(T t)
        {
            items.Add(t);
        }

        // "ConcreteIterator" 

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }

        public IEnumerable<T> FrontToBack
        {
            get
            {
                return this;
            }
        }

        public IEnumerable<T> BackToFront
        {
            get
            {
                for (int i = Count - 1; i >= 0; i--)
                {
                    yield return items[i];
                }
            }
        }

        public IEnumerable<T> FromToStep(int from, int to, int step)
        {
            for (int i = from; i <= to; i = i + step)
            {
                yield return items[i];
            }
        }

        // Property
        public int Count
        {
            get { return items.Count; }
        }

        // System.Collections.IEnumerable member implementation
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Item
    {
        string name;

        // Constructor
        public Item(string name)
        {
            this.name = name;
        }

        // Property
        public string Name
        {
            get { return name; }
        }
    }
}
