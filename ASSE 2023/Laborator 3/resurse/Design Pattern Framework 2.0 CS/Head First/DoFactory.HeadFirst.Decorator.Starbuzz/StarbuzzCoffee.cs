using System;

namespace DoFactory.HeadFirst.Decorator.Starbuzz
{
    class StarbuzzCoffee
    {
        static void Main(string[] args)
        {
            Beverage beverage = new Espresso();
            
            Console.WriteLine(beverage.Description
                + " $" + beverage.Cost);
 
            Beverage beverage2 = new DarkRoast();
            beverage2 = new Mocha(beverage2);
            beverage2 = new Mocha(beverage2);
            beverage2 = new Whip(beverage2);
            Console.WriteLine(beverage2.Description
                + " $" + beverage2.Cost);
 
            Beverage beverage3 = new HouseBlend();
            beverage3 = new Soy(beverage3);
            beverage3 = new Mocha(beverage3);
            beverage3 = new Whip(beverage3);
            Console.WriteLine(beverage3.Description 
                + " $" + beverage3.Cost);

            // Wait for user
            Console.Read();
        }
    }

    #region Beverage

    public abstract class Beverage 
    {
        string description = "Unknown Beverage";
  
        public virtual string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public abstract double Cost
        {
            get;
        }
    }

    public class DarkRoast : Beverage 
    {
        public DarkRoast() 
        {
            Description = "Dark Roast Coffee";
        }

        public override double Cost
        {
            get
            {
                return .99;
            }
        }
    }

    public class Decaf : Beverage 
    {
        public Decaf() 
        {
            Description = "Decaf Coffee";
        }
 
        public override double Cost 
        {
            get
            {
                return 1.05;
            }
        }
    }

    public class Espresso : Beverage 
    {
        public Espresso() 
        {
            Description = "Espresso";
        }
  
        public override double Cost
        {
            get
            {
                return 1.99;
            }
        }
    }

    public class HouseBlend : Beverage 
    {
        public HouseBlend() 
        {
            Description = "House Blend Coffee";
        }
 
        public override double Cost
        {
            get
            {
                return .89;
            }
        }
    }

    #endregion
    
    #region CondimentDecorator

    public abstract class CondimentDecorator : Beverage 
    {
    }

    public class Whip : CondimentDecorator 
    {
        Beverage beverage;

        public Whip(Beverage beverage) 
        {
            this.beverage = beverage;
        }
 
        public override string Description 
        {
            get
            {
                return beverage.Description + ", Whip";
            }
            set
            {
                this.Description = value;
            }
        }
 
        public override double Cost
        {
            get
            {
                return .10 + beverage.Cost;
            }
        }
    }

    public class Milk : CondimentDecorator 
    {
        Beverage beverage;

        public Milk(Beverage beverage) 
        {
            this.beverage = beverage;
        }

        public override string Description
        {
            get
            {
                return beverage.Description + ", Milk";
            }
        }

        public override double Cost
        {
            get
            {
                return .10 + beverage.Cost;
            }
        }
    }

    public class Mocha : CondimentDecorator 
    {
        Beverage beverage;

        public Mocha(Beverage beverage) 
        {
            this.beverage = beverage;
        }

        public override string Description
        {
            get
            {
                return beverage.Description + ", Mocha";
            }
        }

        public override double Cost
        {
            get
            {
                return .20 + beverage.Cost;
            }
        }
    }

    public class Soy : CondimentDecorator 
    {
        Beverage beverage;

        public Soy(Beverage beverage) 
        {
            this.beverage = beverage;
        }

        public override string Description
        {
            get
            {
                return beverage.Description + ", Soy";
            }
        }

        public override double Cost
        {
            get
            {
                return .15 + beverage.Cost;
            }
        }
    }

    #endregion
}