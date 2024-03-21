using System;

namespace DoFactory.GangOfFour.Singleton.Structural
{
    /// <summary>
    /// MainApp startup class for Structural
    /// Singleton Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Constructor is protected -- cannot use new
            Singleton s1 = Singleton.Instance();
            Singleton s2 = Singleton.Instance();

            if (s1 == s2)
            {
                Console.WriteLine("Objects are the same instance");
            }

            // Wait for user
            Console.Read();
        }
    }

    // "Singleton"

    class Singleton
    {
        private static Singleton instance;

        // Note: Constructor is 'protected'
        protected Singleton() 
        {
        }

        public static Singleton Instance()
        {
            // Uses lazy initialization
            if (instance == null)
            {
                instance = new Singleton();
            }

            return instance;
        }
    }
}
