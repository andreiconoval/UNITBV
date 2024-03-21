using System;

namespace DoFactory.HeadFirst.Singleton.MultiThreading
{
    class SingletonClient
    {
        static void Main(string[] args)
        {
            Singleton singleton = Singleton.getInstance();
            singleton.SaySomething();

            // .NET singleton threadsafe example.

            EagerSingleton es1 = EagerSingleton.GetInstance();
            EagerSingleton es2 = EagerSingleton.GetInstance();
            EagerSingleton es3 = EagerSingleton.GetInstance();
            
            if (es1 == es2 && es2 == es3)
            {
                Console.WriteLine("Same instance");
            }
            
            // Wait for user
            Console.Read();
        }
    }

    #region Singleton

    public class Singleton 
    {
        private static Singleton uniqueInstance;
        private static object syncLock = new Object();
 
        // other useful instance variables here
 
        private Singleton() {}
 
        public static Singleton getInstance() 
        {
            // Lock entire body of method
            lock(syncLock)
            {
                if (uniqueInstance == null) 
                {
                    uniqueInstance = new Singleton();
                }
                return uniqueInstance;
            }
        }
 
        // other useful methods here
        public void SaySomething()
        {
            Console.WriteLine("I run, therefore I am");
        }
    }

    sealed class EagerSingleton
    { 
        // CLR eagerly initializes static member when class is first used
        // CLR guarantees thread safety for static initialisation
        private static readonly EagerSingleton instance = new EagerSingleton(); 

        // Note: constructor is private
        private EagerSingleton() 
        {
        } 

        public static EagerSingleton GetInstance()
        {
            return instance;
        }
    }
    #endregion
}
