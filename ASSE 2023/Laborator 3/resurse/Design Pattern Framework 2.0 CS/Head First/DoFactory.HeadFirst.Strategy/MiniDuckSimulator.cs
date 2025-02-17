using System;

namespace DoFactory.HeadFirst.Strategy
{
    public class MiniDuckSimulator
    {
        static void Main(string[] args)
        {
            Duck mallard = new MallardDuck();
            mallard.Display();
            mallard.PerformQuack();
            mallard.PerformFly();

            Console.WriteLine("");

            Duck model = new ModelDuck();
            model.Display();
            model.PerformFly();

            model.FlyBehavior = new FlyRocketPowered();
            model.PerformFly();

            // Wait for user input
            Console.Read();
        }
    }
     
    #region Duck

     public abstract class Duck
     {
        private IFlyBehavior flyBehavior;
        private IQuackBehavior quackBehavior;

        public abstract void Display();

        public void PerformFly()
        {
            flyBehavior.Fly();
        }

        public void PerformQuack()
        {
            quackBehavior.Quack();
        }

        public void Swim()
        {
            Console.WriteLine("All ducks float, even decoys!");
        }

        // Properties; improvement over Java's getters and setters
        public IFlyBehavior FlyBehavior
        {
            get
            {
                return flyBehavior;
            }
            set
            {
                flyBehavior = value;
            }
        }

        public IQuackBehavior QuackBehavior
        {
            get
            {
                return quackBehavior;
            }
            set
            {
                quackBehavior = value;
            }
        }
    }

    public class MallardDuck : Duck
    {
        public MallardDuck()
        {
            QuackBehavior = new LoudQuack();
            FlyBehavior = new FlyWithWings();
        }

        override public void Display()
        {
            Console.WriteLine("I'm a real Mallard duck");
        }
    }

    public class ModelDuck : Duck
    {
        public ModelDuck()
        {
            QuackBehavior = new LoudQuack();
            FlyBehavior = new FlyNoWay();
        }

        override public void Display()
        {
            Console.WriteLine("I'm a model duck");
        }
    }

    #endregion

    #region FlyBehavior

    public interface IFlyBehavior
    {
        void Fly();
    }

    public class FlyWithWings : IFlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("I'm flying!!");
        }
    }
    public class FlyNoWay : IFlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("I can't fly");
        }
    }
    public class FlyRocketPowered : IFlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("I'm flying with a rocket!");
        }
    }
    #endregion

    #region QuackBehavior

    public interface IQuackBehavior
    {
        void Quack();
    }

    // Name it LoadQuack to avoid conflict with method name
    public class LoudQuack : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("LoudQuack");
        }
    }

    public class MuteQuack : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("<< Silence >>");
        }
    }

    public class Squeak : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Squeak");
        }
    }
    #endregion
}
