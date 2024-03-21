using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Adapter.EnumIter
{
    class EnumerationIteratorTestDrive
    {
        static void Main(string[] args)
        {
            // Generic List<T>

            List<string> list = new List<string>();
            
            list.Add("One");
            list.Add("Two");
            list.Add("Three");

            IEnumerator<string> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            Console.WriteLine();

            // Using the foreach construct. It's easier to read and maintain.
            // .NET generates the enumerator code under the hood for this loop

            foreach (string item in list)
            {
                Console.WriteLine(item);
            }
            
            Console.WriteLine();

            // Generic Dictionary<TKey,TValue>

            Dictionary<string,string> dictionary = 
                new Dictionary<string,string>(); 

            dictionary.Add("Key1", "One"  );
            dictionary.Add("Key2", "Two"  );
            dictionary.Add("Key3", "Three");
            dictionary.Add("Key4", "Four" );

            foreach (string key in dictionary.Keys)
            {
                Console.WriteLine(key + ": " + dictionary[key]);
            }

            Console.WriteLine();

            // Generic SortedDictionary<TKey,TValue>

            SortedDictionary<string, string> sortedList =
                new SortedDictionary<string, string>();

            // Add in random order
            sortedList.Add("Key4", "Four");
            sortedList.Add("Key1", "One");
            sortedList.Add("Key3", "Three");
            sortedList.Add("Key2", "Two"  );

            foreach (string key in sortedList.Keys)
            {
                Console.WriteLine(key + ": " + sortedList[key]);
            }

            // Wait for user
            Console.Read();
        }
    }
}
