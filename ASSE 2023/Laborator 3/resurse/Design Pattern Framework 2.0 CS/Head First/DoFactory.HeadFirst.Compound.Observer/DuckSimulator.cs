using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Combining.Observer
{
    class DuckSimulator
    {
        static void Main(string[] args)
        {
            DuckSimulator simulator = new DuckSimulator();
            AbstractDuckFactory factory = new CountingDuckFactory();
 
            simulator.Simulate(factory);

            // Wait for user
            Console.Read();
        }
  
        void Simulate(AbstractDuckFactory factory) 
        {
            IQuackable redheadDuck = factory.CreateRedheadDuck();
            IQuackable duckCall    = factory.CreateDuckCall();
            IQuackable rubberDuck  = factory.CreateRubberDuck();
            IQuackable gooseDuck   = new GooseAdapter(new Goose());
 
            Flock flockOfDucks = new Flock();
 
            flockOfDucks.Add(redheadDuck);
            flockOfDucks.Add(duckCall);
            flockOfDucks.Add(rubberDuck);
            flockOfDucks.Add(gooseDuck);
 
            Flock flockOfMallards = new Flock();
 
            IQuackable mallardOne   = factory.CreateMallardDuck();
            IQuackable mallardTwo   = factory.CreateMallardDuck();
            IQuackable mallardThree = factory.CreateMallardDuck();
            IQuackable mallardFour  = factory.CreateMallardDuck();

            flockOfMallards.Add(mallardOne);
            flockOfMallards.Add(mallardTwo);
            flockOfMallards.Add(mallardThree);
            flockOfMallards.Add(mallardFour);

            flockOfDucks.Add(flockOfMallards);

            Console.WriteLine("Duck Simulator: With Observer");

            Quackologist quackologist = new Quackologist();
            flockOfDucks.RegisterObserver(quackologist);

            Simulate(flockOfDucks);

            Console.WriteLine("\nThe ducks quacked " + 
                QuackCounter.Quacks + " times");
        }
 
        void Simulate(IQuackable duck) 
        {
            duck.Quack();
        }
    }

    #region Factory

    public abstract class AbstractDuckFactory 
    {
        public abstract IQuackable CreateMallardDuck();
        public abstract IQuackable CreateRedheadDuck();
        public abstract IQuackable CreateDuckCall();
        public abstract IQuackable CreateRubberDuck();
    }

    public class DuckFactory : AbstractDuckFactory 
    {
        public override IQuackable CreateMallardDuck() 
        {
            return new MallardDuck();
        }
  
        public override IQuackable CreateRedheadDuck() 
        {
            return new RedheadDuck();
        }
  
        public override IQuackable CreateDuckCall() 
        {
            return new DuckCall();
        }
   
        public override IQuackable CreateRubberDuck() 
        {
            return new RubberDuck();
        }
    }

    public class CountingDuckFactory : AbstractDuckFactory 
    {
        public override IQuackable CreateMallardDuck() 
        {
            return new QuackCounter(new MallardDuck());
        }
  
        public override IQuackable CreateRedheadDuck() 
        {
            return new QuackCounter(new RedheadDuck());
        }
  
        public override IQuackable CreateDuckCall() 
        {
            return new QuackCounter(new DuckCall());
        }
   
        public override IQuackable CreateRubberDuck() 
        {
            return new QuackCounter(new RubberDuck());
        }
    }

    #endregion

    #region Quack Counter

    public class QuackCounter : IQuackable 
    {
        IQuackable duck;
        static int numberOfQuacks;
  
        public QuackCounter(IQuackable duck) 
        {
            this.duck = duck;
        }
  
        public void Quack() 
        {
            duck.Quack();
            numberOfQuacks++;
        }
 
        public static int Quacks 
        {
            get{ return numberOfQuacks; }
        }
 
        public void RegisterObserver(IObserver observer) 
        {
            duck.RegisterObserver(observer);
        }
 
        public void NotifyObservers() 
        {
            duck.NotifyObservers();
        }
   
        public override string ToString() 
        {
            return duck.ToString();
        }
    }

    #endregion

    #region Observer

    public interface IObserver 
    {
        void Update(IQuackObservable duck);
    }

    public class Quackologist : IObserver 
    {
        public void Update(IQuackObservable duck) 
        {
            Console.WriteLine("Quackologist: " + duck + " just quacked.");
        }
 
        public override string ToString() 
        {
            return "Quackologist";
        }
    }

    public class Observable : IQuackObservable 
    {
        // Generic List<T> of IObservers
        List<IObserver> observers = new List<IObserver>();
        IQuackObservable duck;
 
        public Observable(IQuackObservable duck) 
        {
            this.duck = duck;
        }
  
        public void RegisterObserver(IObserver observer) 
        {
            observers.Add(observer);
        }
  
        public void NotifyObservers() 
        {
            foreach(IObserver observer in observers)
            {
                observer.Update(duck);
            }
        }
    }

    #endregion

    #region Flock 

    public class Flock : IQuackable 
    {
        List<IQuackable> ducks = new List<IQuackable>();
  
        public void Add(IQuackable duck) 
        {
            ducks.Add(duck);
        }
  
        public void Quack() 
        {
            foreach(IQuackable duck in ducks)
            {
                duck.Quack();
            }
        }
   
        public void RegisterObserver(IObserver observer) 
        {
            foreach(IQuackable duck in ducks)
            {
                duck.RegisterObserver(observer);
            }
        }
  
        public void NotifyObservers() { }
  
        public override string ToString() 
        {
            return "Flock of Ducks";
        }
    }

    #endregion

    #region Ducks

    public interface IQuackObservable 
    {
        void RegisterObserver(IObserver observer);
        void NotifyObservers();
    }

    public interface IQuackable : IQuackObservable 
    {
        void Quack();
    }

    public class RubberDuck : IQuackable 
    {
        Observable observable;

        public RubberDuck() 
        {
            observable = new Observable(this);
        }
 
        public void Quack() 
        {
            Console.WriteLine("Squeak");
            NotifyObservers();
        }

        public void RegisterObserver(IObserver observer) 
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            observable.NotifyObservers();
        }
  
        public override string ToString() 
        {
            return "Rubber Duck";
        }
    }

    public class MallardDuck : IQuackable 
    {
        Observable observable;
 
        public MallardDuck() 
        {
            observable = new Observable(this);
        }
 
        public void Quack() 
        {
            Console.WriteLine("Quack");
            NotifyObservers();
        }
 
        public void RegisterObserver(IObserver observer) 
        {
            observable.RegisterObserver(observer);
        }
 
        public void NotifyObservers() 
        {
            observable.NotifyObservers();
        }
 
        public override string ToString() 
        {
            return "Mallard Duck";
        }
    }
    public class RedheadDuck : IQuackable 
    {
        Observable observable;

        public RedheadDuck() 
        {
            observable = new Observable(this);
        }

        public void Quack() 
        {
            Console.WriteLine("Quack");
            NotifyObservers();
        }

        public void RegisterObserver(IObserver observer) 
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            observable.NotifyObservers();
        }

        public override string ToString() 
        {
            return "Redhead Duck";
        }
    }

    public class DuckCall : IQuackable 
    {
        Observable observable;

        public DuckCall() 
        {
            observable = new Observable(this);
        }
 
        public void Quack() 
        {
            Console.WriteLine("Kwak");
            NotifyObservers();
        }
 
        public void RegisterObserver(IObserver observer) 
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            observable.NotifyObservers();
        }
 
        public override string ToString() 
        {
            return "Duck Call";
        }
    }

    public class DecoyDuck : IQuackable 
    {
        Observable observable;

        public DecoyDuck() 
        {
            observable = new Observable(this);
        }
 
        public void Quack() 
        {
            Console.WriteLine("<< Silence >>");
            NotifyObservers();
        }
 
        public void RegisterObserver(IObserver observer) 
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            observable.NotifyObservers();
        }
 
        public override string ToString() 
        {
            return "Decoy Duck";
        }
    }

    #endregion

    #region Goose

    public class Goose 
    {
        public void Honk() 
        {
            Console.WriteLine("Honk");
        }

        public override string ToString() 
        {
            return "Goose";
        }
    }

    public class GooseAdapter : IQuackable 
    {
        Goose goose;
        Observable observable;

        public GooseAdapter(Goose goose) 
        {
            this.goose = goose;
            observable = new Observable(this);
        }
 
        public void Quack() 
        {
            goose.Honk();
            NotifyObservers();
        }

        public void RegisterObserver(IObserver observer) 
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            observable.NotifyObservers();
        }

        public override string ToString() 
        {
            return "Goose pretending to be a Duck";
        }
    }
    #endregion
}
