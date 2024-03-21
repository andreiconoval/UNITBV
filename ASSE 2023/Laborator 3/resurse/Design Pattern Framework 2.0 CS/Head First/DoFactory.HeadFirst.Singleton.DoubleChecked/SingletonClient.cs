using System;

namespace DoFactory.HeadFirst.Singleton.DoubleChecked
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

    public class Singleton 
    {
        private volatile static Singleton uniqueInstance;
        private static object syncLock = new object();
 
        private Singleton() {}
 
        public static Singleton getInstance() 
        {
            if (uniqueInstance == null) 
            {
                // Lock area where instance is created
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

        public void SaySomething()
        {
            Console.WriteLine("I am double checked, therefore I am");
        }
    }
    #endregion
}
