using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Composite.Menu
{
    class MenuTestDrive
    {
        static void Main(string[] args)
        {
            MenuComponent pancakeHouseMenu = 
                new Menu("PANCAKE HOUSE MENU", "Breakfast");
            MenuComponent dinerMenu = 
                new Menu("DINER MENU", "Lunch");
            MenuComponent cafeMenu = 
                new Menu("CAFE MENU", "Dinner");
            MenuComponent dessertMenu = 
                new Menu("DESSERT MENU", "Dessert of course!");
            MenuComponent coffeeMenu = new Menu("COFFEE MENU", "Stuff to go with your afternoon coffee");
  
            MenuComponent allMenus = new Menu("ALL MENUS", "All menus combined");
  
            allMenus.Add(pancakeHouseMenu);
            allMenus.Add(dinerMenu);
            allMenus.Add(cafeMenu);
  
            pancakeHouseMenu.Add(new MenuItem(
                "K&B's Pancake Breakfast", 
                "Pancakes with scrambled eggs, and toast", 
                true,
                2.99));
            pancakeHouseMenu.Add(new MenuItem(
                "Regular Pancake Breakfast", 
                "Pancakes with fried eggs, sausage", 
                false,
                2.99));
            pancakeHouseMenu.Add(new MenuItem(
                "Blueberry Pancakes",
                "Pancakes made with fresh blueberries, and blueberry syrup",
                true,
                3.49));
            pancakeHouseMenu.Add(new MenuItem(
                "Waffles",
                "Waffles, with your choice of blueberries or strawberries",
                true,
                3.59));

            dinerMenu.Add(new MenuItem(
                "Vegetarian BLT",
                "(Fakin') Bacon with lettuce & tomato on whole wheat", 
                true, 
                2.99));
            dinerMenu.Add(new MenuItem(
                "BLT",
                "Bacon with lettuce & tomato on whole wheat", 
                false, 
                2.99));
            dinerMenu.Add(new MenuItem(
                "Soup of the day",
                "A bowl of the soup of the day, with a side of potato salad", 
                false, 
                3.29));
            dinerMenu.Add(new MenuItem(
                "Hotdog",
                "A hot dog, with saurkraut, relish, onions, topped with cheese",
                false, 
                3.05));
            dinerMenu.Add(new MenuItem(
                "Steamed Veggies and Brown Rice",
                "Steamed vegetables over brown rice", 
                true, 
                3.99));
             dinerMenu.Add(new MenuItem(
                "Pasta",
                "Spaghetti with Marinara Sauce, and a slice of sourdough bread",
                true, 
                3.89));

            dinerMenu.Add(dessertMenu);

            dessertMenu.Add(new MenuItem(
                "Apple Pie",
                "Apple pie with a flakey crust, topped with vanilla icecream",
                true,
                1.59));
             dessertMenu.Add(new MenuItem(
                "Cheesecake",
                "Creamy New York cheesecake, with a chocolate graham crust",
                true,
                1.99));
            dessertMenu.Add(new MenuItem(
                "Sorbet",
                "A scoop of raspberry and a scoop of lime",
                true,
                1.89));
            cafeMenu.Add(new MenuItem(
                "Veggie Burger and Air Fries",
                "Veggie burger on a whole wheat bun, lettuce, tomato, and fries",
                true, 
                3.99));
            cafeMenu.Add(new MenuItem(
                "Soup of the day",
                "A cup of the soup of the day, with a side salad",
                false, 
                3.69));
            cafeMenu.Add(new MenuItem(
                "Burrito",
                "A large burrito, with whole pinto beans, salsa, guacamole",
                true, 
                4.29));

            cafeMenu.Add(coffeeMenu);

            coffeeMenu.Add(new MenuItem(
                "Coffee Cake",
                "Crumbly cake topped with cinnamon and walnuts",
                true,
                1.59));
            coffeeMenu.Add(new MenuItem(
                "Bagel",
                "Flavors include sesame, poppyseed, cinnamon raisin, pumpkin",
                false,
                0.69));
            coffeeMenu.Add(new MenuItem(
                "Biscotti",
                "Three almond or hazelnut biscotti cookies",
                true,
                0.89));
 
            Waitress waitress = new Waitress(allMenus);
            waitress.PrintMenu();

            // Wait for user
            Console.Read();
        }    
    }

    #region Waitress

    public class Waitress 
    {
        MenuComponent allMenus;
 
        public Waitress(MenuComponent allMenus) 
        {
            this.allMenus = allMenus;
        }
 
        public void PrintMenu() 
        {
            allMenus.Print();
        }
    }

    #endregion

    #region Menu

    public abstract class MenuComponent 
    {
        public virtual void Add(MenuComponent menuComponent)
        {
            throw new NotSupportedException(); 
        }

        public virtual void Remove(MenuComponent menuComponent)
        {
            throw new NotSupportedException(); 
        }

        public virtual MenuComponent GetChild(int i)
        {
            throw new NotSupportedException(); 
        }
  
        public virtual string Name
        {
            get
            {
                throw new NotSupportedException(); 
            }
        }

        public virtual string Description
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public virtual double Price 
        {
            get
            {
                throw new NotSupportedException(); 
            }
        }

        public virtual bool Vegetarian
        {
            get
            {
                throw new NotSupportedException();
            }
        }
  
        public virtual void Print()
        {
            throw new NotSupportedException();
        }
    }

    public class MenuItem : MenuComponent 
    {
        private string name;
        private string description;
        private bool vegetarian;
        private double price;
    
        // Constructor
        public MenuItem(string name, string description, bool vegetarian, double price) 
        { 
            this.name = name;
            this.description = description;
            this.vegetarian = vegetarian;
            this.price = price;
        }
  
        public override string Name 
        {
            get{ return name; }
        }
  
        public override string Description 
        {
            get{ return description; }
        }
  
        public override double Price 
        {
            get{ return price; }
        }
  
        public override bool Vegetarian 
        {
            get{ return vegetarian; }
        }
  
        public override void Print() 
        {
            Console.Write(Name);
            if (Vegetarian) 
            {
                Console.Write("(v)");
            }
            Console.Write(", " + Price);
            Console.WriteLine("\n -- " + Description);
        }
    }

    public class Menu : MenuComponent 
    {
        // Generic List<T> of MenuComponents
        private List<MenuComponent> menuComponents = new List<MenuComponent>();
        private string name;
        private string description;
  
        public Menu(string name, string description) 
        {
            this.name = name;
            this.description = description;
        }
 
        public override void Add(MenuComponent menuComponent) 
        {
            menuComponents.Add(menuComponent);
        }
 
        public override void Remove(MenuComponent menuComponent) 
        {
            menuComponents.Remove(menuComponent);
        }
 
        public override MenuComponent GetChild(int i) 
        {
            return menuComponents[i];
        }
 
        public override string Name 
        {
            get{ return name; }
        }
 
        public override string Description 
        {
            get{ return description; }
        }
 
        public override void Print() 
        {
            Console.Write("\n" + Name);
            Console.WriteLine(", " + Description);
            Console.WriteLine("---------------------");
  
            foreach(MenuComponent menu in menuComponents)
            {
                menu.Print();
            }
        }
    }
    #endregion
}
