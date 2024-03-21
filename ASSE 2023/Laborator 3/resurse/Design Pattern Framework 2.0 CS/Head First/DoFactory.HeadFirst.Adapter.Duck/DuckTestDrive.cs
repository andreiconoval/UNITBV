using System;

namespace DoFactory.HeadFirst.Adapter.Ducks
{
    class DuckTestDrive
    {
        static void Main(string[] args)
        {
            // Test 1: Duck test drive

            MallardDuck duck = new MallardDuck();

            WildTurkey turkey = new WildTurkey();
            IDuck turkeyAdapter = new TurkeyAdapter(turkey);
  
            Console.WriteLine("The Turkey says...");
            turkey.Gobble();
            turkey.Fly();

            Console.WriteLine("\nThe Duck says...");
            TestDuck(duck);

            Console.WriteLine("\nThe TurkeyAdapter says...");
            TestDuck(turkeyAdapter);        

            // Test 2: Turkey test drive

            ITurkey duckAdapter = new DuckAdapter(duck);

            for (int i = 0; i < 10; i++) 
            {
                Console.WriteLine("The DuckAdapter says...");
                duckAdapter.Gobble();
                duckAdapter.Fly();
            }

            // Wait for user
            Console.Read();
        }

        static void TestDuck(IDuck duck) 
        {
            duck.Quack();
            duck.Fly();
        }
    }

    public interface IDuck 
    {
        void Quack();
        void Fly();
    }

    public interface ITurkey 
    {
        void Gobble();
        void Fly();
    }

    public class MallardDuck : IDuck 
    {
        public void Quack() 
        {
            Console.WriteLine("Quack");
        }

        public void Fly() 
        {
            Console.WriteLine("I'm flying");
        }
    }

    public class WildTurkey : ITurkey 
    {
        public void Gobble() 
        {
            Console.WriteLine("Gobble gobble");
        }

        public void Fly() 
        {
            Console.WriteLine("I'm flying a short distance");
        }
    }

    public class DuckAdapter : ITurkey 
    {
        IDuck duck;
        Random rand;

        public DuckAdapter(IDuck duck) 
        {
            this.duck = duck;
            rand = new Random();
        }
  
        public void Gobble() 
        {
            duck.Quack();
        }

        public void Fly() 
        {
            if (rand.Next(5) == 0) 
            {
                duck.Fly();
            }
        }
    }

    public class TurkeyAdapter : IDuck 
    {
        ITurkey turkey;

        public TurkeyAdapter(ITurkey turkey) 
        {
            this.turkey = turkey;
        }
  
        public void Quack() 
        {
            turkey.Gobble();
        }

        public void Fly() 
        {
            for (int i = 0; i < 5; i++) 
            {
                turkey.Fly();
            }
        }
    }
}
