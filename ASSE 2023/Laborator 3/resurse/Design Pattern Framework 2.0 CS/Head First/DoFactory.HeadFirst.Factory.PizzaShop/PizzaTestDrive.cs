using System;
using System.Text;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Factory.PizzaShop
{
    class PizzaTestDrive
    {
        static void Main(string[] args)
        {
            SimplePizzaFactory factory = new SimplePizzaFactory();
            PizzaStore store = new PizzaStore(factory);

            Pizza pizza = store.OrderPizza("cheese");
            Console.WriteLine("We ordered a " + pizza.Name + "\n");
 
            pizza = store.OrderPizza("veggie");
            Console.WriteLine("We ordered a " + pizza.Name + "\n");        

            // Wait for user
            Console.Read();
        }
    }
    #region PizzaStore

    public class PizzaStore 
    {
        SimplePizzaFactory factory;
 
        public PizzaStore(SimplePizzaFactory factory) 
        { 
            this.factory = factory;
        }
 
        public Pizza OrderPizza(string type) 
        {
            Pizza pizza = factory.CreatePizza(type);
 
            pizza.Prepare();
            pizza.Bake();
            pizza.Cut();
            pizza.Box();

            return pizza;
        }
    }

    #endregion

    #region SimplePizzaFactory

    public class SimplePizzaFactory 
    {
        public Pizza CreatePizza(string type) 
        {
            Pizza pizza = null;
            switch(type)
            {
                case "cheese": pizza = new CheesePizza();    break;
                case "pepperoni": pizza = new PepperoniPizza();break;
                case "clam": pizza = new ClamPizza();break;
                case "veggie": pizza = new VeggiePizza();break;
            }
            Console.WriteLine(pizza);
            return pizza;
        }
    }
    
    #endregion

    #region Pizza

    abstract public class Pizza 
    {
        private string name;
        private string dough;
        private string sauce;
        private List<string> toppings = new List<string>();

        public Pizza(string name, string dough, string sauce)
        {
            this.name = name;
            this.dough = dough;
            this.sauce = sauce;
        }

        public string Name
        {
            get{ return name; }
            set{ name = value; }
        }

        public List<string> Toppings
        {
            get{ return toppings; }
        }

        public void Prepare() 
        {
            Console.WriteLine("Preparing " + name);
        }

        public void Bake() 
        {
            Console.WriteLine("Baking " + name);
        }

        public void Cut() 
        {
            Console.WriteLine("Cutting " + name);
        }

        public void Box() 
        {
            Console.WriteLine("Boxing " + name);
        }

        // code to display pizza name and ingredients
        public override string ToString() 
        {
            StringBuilder display = new StringBuilder();
            display.Append("---- " + name + " ----\n");
            display.Append(dough + "\n");
            display.Append(sauce + "\n");
            foreach(string topping in toppings)
            {
                display.Append(topping + "\n");
            }

            return display.ToString();
        }
    }

    public class CheesePizza : Pizza 
    {
        public CheesePizza() :
            base("Cheese Pizza","Regular Crust","Marinara Pizza Sauce")
        {
            Toppings.Add("Fresh Mozzarella");
            Toppings.Add("Parmesan");
        }
    }

    public class VeggiePizza : Pizza 
    {
        public VeggiePizza() : 
            base( "Veggie Pizza", "Crust", "Marinara sauce" )
        {
            Toppings.Add("Shredded mozzarella");
            Toppings.Add("Grated parmesan");
            Toppings.Add("Diced onion");
            Toppings.Add("Sliced mushrooms");
            Toppings.Add("Sliced red pepper");
            Toppings.Add("Sliced black olives");
        }
    }

    public class PepperoniPizza : Pizza 
    {
        public PepperoniPizza() : 
            base("Pepperoni Pizza", "Crust", "Marinara sauce" )
        {
            Toppings.Add("Sliced Pepperoni");
            Toppings.Add("Sliced Onion");
            Toppings.Add("Grated parmesan cheese");
        }
    }

    public class ClamPizza : Pizza 
    {
        public ClamPizza() :
         base("Clam Pizza", "Thin crust", "White garlic sauce" )
        {
            Toppings.Add("Clams");
            Toppings.Add("Grated parmesan cheese");
        }
    }
    #endregion
}
