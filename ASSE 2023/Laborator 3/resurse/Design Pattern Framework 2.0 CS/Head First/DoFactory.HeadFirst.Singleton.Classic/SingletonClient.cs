using System;

namespace DoFactory.HeadFirst.Singleton.Classic
{
    class SingletonClient
    {
        static void Main(string[] args)
        {
            Singleton singleton = Singleton.getInstance();
            singleton.SaySomething();

            // Wait for user
            Console.Read();
        }
    }

    #region Singleton

    // NOTE: This is not thread safe

    public class Singleton 
    {
        private static Singleton uniqueInstance;
 
        // other useful instance variables here

        // Constructor
        private Singleton() {}
 
        public static Singleton getInstance() 
        {
            if (uniqueInstance == null) 
            {
                uniqueInstance = new Singleton();
            }
            return uniqueInstance;
        }

        public void SaySomething()
        {
            Console.WriteLine("Hi, here I am");
        }
 
        // other useful methods here
    }
    #endregion
}
