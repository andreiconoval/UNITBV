using System;

namespace DoFactory.HeadFirst.Singleton.Chocolate
{
    class ChocolateController
    {
        static void Main(string[] args)
        {
            ChocolateBoiler boiler = ChocolateBoiler.GetInstance();
            boiler.Fill();
            boiler.Boil();
            boiler.Drain();

            // will return the existing instance
            ChocolateBoiler boiler2 = ChocolateBoiler.GetInstance();

            // Are they the same?
            if (boiler == boiler2)
            {
                Console.WriteLine("Same instances");
            }

            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();
            Singleton s3 = Singleton.GetInstance();

            if (s1 == s2 && s2 == s3)
            {
                Console.WriteLine("Same instances");
            }

            // Wait for user
            Console.Read();
        }
    }
    
    #region ChocolateBoiler

    public class ChocolateBoiler
    {
        public static ChocolateBoiler uniqueInstance;

        private bool empty;
        private bool boiled;

        // Private constructor
        private ChocolateBoiler()
        {
            empty = true;
            boiled = false;
        }

        public static ChocolateBoiler GetInstance()
        {
            if (uniqueInstance == null) 
            {
                Console.WriteLine("Creating unique instance of Chocolate Boiler");
                uniqueInstance = new ChocolateBoiler();
            }

            Console.WriteLine("Returning instance of Chocolate Boiler");
            return uniqueInstance;
        }

        public void Fill() 
        {
            if (Empty) 
            {
                empty = false;
                boiled = false;
                // fill the boiler with a milk/chocolate mixture
            }
        }
 
        public void Drain() 
        {
            if (Empty && Boiled) 
            {
                // drain the boiled milk and chocolate
                empty = true;
            }
        }
 
        public void Boil() 
        {
            if (!Empty && !Boiled) 
            {
                // bring the contents to a boil
                boiled = true;
            }
        }
  
        // Properties 
        public bool Empty 
        {
            get{ return empty; }
        }
 
        public bool Boiled 
        {
            get{ return boiled; }
        }
    }
    #endregion

    #region Singleton

    public class Singleton 
    {
        private static Singleton uniqueInstance;
 
        // other useful instance variables here
 
        // Constructor
        private Singleton() {}

        private static object syncLock = new object();
 
        public static Singleton GetInstance() 
        {
            // Double checked locking
            if (uniqueInstance == null)
            {
                lock(syncLock)
                {
                    if (uniqueInstance == null) 
                    {
                        uniqueInstance = new Singleton();
                    }
                }
            }
            return uniqueInstance;
        }
 
        // other useful methods here
    }
    #endregion
}
