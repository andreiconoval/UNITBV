using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Composite.MenuIterator
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
  
            MenuComponent allMenus = new Menu("ALL MENUS", "All menus combined");
  
            allMenus.Add(pancakeHouseMenu);
            allMenus.Add(dinerMenu);
            allMenus.Add(cafeMenu);
            allMenus.Add(dessertMenu);
  
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
                "A medly of steamed vegetables over brown rice", 
                true, 
                3.99));
             dinerMenu.Add(new MenuItem(
                "Pasta",
                "Spaghetti with Marinara Sauce, and a slice of sourdough bread",
                true, 
                3.89));
   
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

            Console.WriteLine("\nVEGETARIAN MENU\n----");
 
            Waitress waitress = new Waitress();
            waitress.PrintVegetarianMenu(allMenus);
 
            // Wait for user
            Console.Read();
        }
    }

    #region Waitress

    public class Waitress 
    {
        public void PrintVegetarianMenu(MenuComponent menuComponent) 
        {
            IEnumerator<MenuComponent> iterator = menuComponent.CreateIterator();
            
            while (iterator.MoveNext()) 
            {
                MenuComponent menu = iterator.Current;
                if (menu is Menu)
                {
                    PrintVegetarianMenu( menu );
                }
                else if (menu.IsVegetarian()) 
                {
                        menu.Print();
                }
            }
        }
    }

    #endregion

    #region Menu and MenuItems

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
  
        public virtual string GetName() 
        {
            throw new NotSupportedException();
        }

        public virtual string getDescription() 
        {
            throw new NotSupportedException();
        }

        public virtual double GetPrice() 
        {
            throw new NotSupportedException();
        }

        public virtual bool IsVegetarian() 
        {
            return false; 
        }

        public abstract IEnumerator<MenuComponent> CreateIterator();
  
        public virtual void Print() 
        {
            throw new NotSupportedException();
        }
    }

    public class Menu : MenuComponent 
    {
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
 
        public override string GetName() 
        {
            return name;
        }
 
        public string GetDescription() 
        {
            return description;
        }
 
        public override void Print() 
        {
            Console.Write(GetName());
            Console.Write(", " + GetDescription());
            Console.WriteLine("---------------------");
  
            foreach (MenuComponent mc in menuComponents)
            {
                mc.Print();
            }
        }

        public override IEnumerator<MenuComponent> CreateIterator() 
        {
            return menuComponents.GetEnumerator();
        }
    }

    public class MenuItem : MenuComponent 
    {
        public string name;
        public string description;
        public bool vegetarian;
        public double price;
    
        public MenuItem(String name, 
            String description, 
            bool vegetarian, 
            double price) 
        { 
            this.name = name;
            this.description = description;
            this.vegetarian = vegetarian;
            this.price = price;
        }
  
        public override string GetName() 
        {
            return name;
        }
  
        public string GetDescription() 
        {
            return description;
        }
  
        public override double GetPrice() 
        {
            return price;
        }
  
        public override bool IsVegetarian() 
        {
            return vegetarian;
        }

        public override IEnumerator<MenuComponent> CreateIterator() 
        {
            return null; //new NullIterator();
        }
  
        public override void Print() 
        {
            Console.Write("  " + GetName());
            if (IsVegetarian()) 
            {
                Console.Write("(v)");
            }
            Console.WriteLine(", " + GetPrice());
            Console.WriteLine("     -- " + GetDescription());
        }
    }

    #endregion
}

