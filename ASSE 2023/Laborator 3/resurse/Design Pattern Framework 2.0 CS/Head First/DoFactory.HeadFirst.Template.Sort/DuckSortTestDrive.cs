using System;

namespace DoFactory.HeadFirst.Template.Sort
{
    class DuckSortTestDrive
    {
        static void Main(string[] args)
        {
            Duck[] ducks = { 
                    new Duck("Daffy", 8), 
                    new Duck("Dewey", 2),
                    new Duck("Howard", 7),
                    new Duck("Louie", 2),
                    new Duck("Donald", 10), 
                    new Duck("Huey", 2)};

            Console.WriteLine("Before sorting:");
            Display(ducks);

            Array.Sort(ducks);
     
            Console.WriteLine("\nAfter sorting:");
            Display(ducks);        

            // Wait for user
            Console.Read();
        }

        public static void Display(Duck[] ducks)
        {
            foreach (Duck duck in ducks)
            {
                Console.WriteLine(duck);
            }
        }
    }

    #region Duck

    public class Duck : IComparable 
    {
        string name;
        int weight;
  
        public Duck(string name, int weight) 
        {
            this.name = name;
            this.weight = weight;
        }
 
        public override string ToString() 
        {
            return name + " weighs " + weight;
        }
  
        public int CompareTo(Object o) 
        {
             Duck otherDuck = (Duck)o;
  
            if (this.weight < otherDuck.weight) 
            {
                return -1;
            } 
            else if (this.weight == otherDuck.weight) 
            {
                return 0;
            } 
            else // this.weight > otherDuck.weight
            { 
                return 1;
            }
        }
    }
    #endregion
}
