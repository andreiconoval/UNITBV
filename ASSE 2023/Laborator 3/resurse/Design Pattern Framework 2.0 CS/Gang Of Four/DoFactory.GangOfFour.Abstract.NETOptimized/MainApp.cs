using System;

namespace DoFactory.GangOfFour.Abstract.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Abstract Factory Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        public static void Main()
        {
            // Create and run the Africa animal world
            AnimalWorld world = new AnimalWorld(Continent.Africa);
            world.RunFoodChain();

            // Create and run the America animal world
            world = new AnimalWorld(Continent.America);
            world.RunFoodChain();

            // Wait for user input
            Console.Read();
        }
    }

    /// <summary>
    /// The 'AbstractFactory' interface. 
    /// </summary>
    interface IContinentFactory
    {
        IHerbivore CreateHerbivore();
        ICarnivore CreateCarnivore();
    }

    /// <summary>
    /// The 'ConcreteFactory1' class.
    /// </summary>
    class AfricaFactory : IContinentFactory
    {
        public IHerbivore CreateHerbivore()
        {
            return new Wildebeest();
        }
        public ICarnivore CreateCarnivore()
        {
            return new Lion();
        }
    }

    /// <summary>
    /// The 'ConcreteFactory2' class.
    /// </summary>
    class AmericaFactory : IContinentFactory
    {
        public IHerbivore CreateHerbivore()
        {
            return new Bison();
        }
        public ICarnivore CreateCarnivore()
        {
            return new Wolf();
        }
    }

    /// <summary>
    /// "AbstractProductA" 
    /// </summary>
    interface IHerbivore
    {
    }

    /// <summary>
    /// "AbstractProductB"
    /// </summary>
    interface ICarnivore
    {
        void Eat(IHerbivore h);
    }

    /// <summary>
    /// The "ProductA1" class
    /// </summary>
    class Wildebeest : IHerbivore
    {
    }

    /// <summary>
    /// The "ProductB1" class
    /// </summary>
    class Lion : ICarnivore
    {
        public void Eat(IHerbivore h)
        {
            // Eat Wildebeest
            Console.WriteLine(this.GetType().Name + 
                " eats " + h.GetType().Name);
        }
    }

    /// <summary>
    /// The "ProductA2" class
    /// </summary>
    class Bison : IHerbivore
    {
    }

    /// <summary>
    /// The "ProductB2" class
    /// </summary>
    class Wolf : ICarnivore
    {
        public void Eat(IHerbivore h)
        {
            // Eat Bison
            Console.WriteLine(this.GetType().Name + 
                " eats " + h.GetType().Name);
        }
    }

    /// <summary>
    /// The 'Client' class (according to pattern UML).
    /// </summary>
    class AnimalWorld
    {
        private IHerbivore herbivore;
        private ICarnivore carnivore;

        /// <summary>
        /// Contructor of Animalworld
        /// </summary>
        /// <param name="continent">Continent of the animal world that is created.</param>
        public AnimalWorld(Continent continent)
        {
            // Get fully qualified factory name
            string name = this.GetType().Namespace + "." + 
                continent.ToString() + "Factory";
            
            // Dynamic factory creation
            IContinentFactory factory = 
                (IContinentFactory)System.Activator.CreateInstance
                (Type.GetType(name));
            carnivore = factory.CreateCarnivore();
            herbivore = factory.CreateHerbivore();
        }

        /// <summary>
        /// Runs the foodchain in the animal world in which carnivores eat herbivores.
        /// </summary>
        public void RunFoodChain()
        {
            carnivore.Eat(herbivore);
        }
    }

    /// <summary>
    /// Enumeration of continents.
    /// </summary>
    public enum Continent
    {
        /// <summary>
        /// Represents continent of Africa.
        /// </summary>
        Africa,

        /// <summary>
        /// Represents continent of America.
        /// </summary>
        America,
        
        /// <summary>
        /// Represents continent of Asia.
        /// </summary>
        Asia,

        /// <summary>
        /// Represents continent of Europe.
        /// </summary>
        Europe,

        /// <summary>
        /// Represents continent of Australia.
        /// </summary>
        Australia
    }
}
