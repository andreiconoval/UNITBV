using System;

namespace DoFactory.GangOfFour.Bridge.Structural
{
    /// <summary>
    /// MainApp startup class for Structural
    /// Bridge Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            Abstraction ab = new RefinedAbstraction();

            // Set implementation and call
            ab.Implementor = new ConcreteImplementorA();
            ab.Operation();

            // Change implemention and call
            ab.Implementor = new ConcreteImplementorB();
            ab.Operation();

            // Wait for user
            Console.Read();
        }
    }

    // "Abstraction"

    class Abstraction
    {
        protected Implementor implementor;

        // Property
        public Implementor Implementor
        {
            set{ implementor = value; }
        }

        public virtual void Operation()
        {
            implementor.Operation();
        }
    }

    // "Implementor" 

    abstract class Implementor
    {
        public abstract void Operation();
    }

    // "RefinedAbstraction"

    class RefinedAbstraction : Abstraction
    {
        public override void Operation()
        {
            implementor.Operation();
        }
    }

    // "ConcreteImplementorA"

    class ConcreteImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorA Operation");
        }
    }

    // "ConcreteImplementorB"

    class ConcreteImplementorB : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorB Operation");
        }
    }
}
