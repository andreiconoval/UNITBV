using System;
using System.Collections;

namespace DoFactory.HeadFirst.Iterator.DinerMergerCafe
{
    class MenuTestDrive
    {
        static void Main(string[] args)
        {
            PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();
            CafeMenu cafeMenu = new CafeMenu();
 
            Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu, cafeMenu);
 
            waitress.PrintMenu();
            waitress.PrintVegetarianMenu();

            Console.WriteLine("\nCustomer asks, is the Hotdog vegetarian?");
            Console.Write("Waitress says: ");
            if (waitress.IsItemVegetarian("Hotdog")) 
            {
                Console.WriteLine("Yes");
            } 
            else 
            {
                Console.WriteLine("No");
            }

            Console.WriteLine("\nCustomer asks, are the Waffles vegetarian?");
            Console.Write("Waitress says: ");
            if (waitress.IsItemVegetarian("Waffles")) 
            {
                Console.WriteLine("Yes");
            } 
            else 
            {
                Console.WriteLine("No");
            }

            // Wait for user
            Console.Read();
        }
    }

    #region Waitress

    public class Waitress 
    {
        IMenu pancakeHouseMenu;
        IMenu dinerMenu;
        IMenu cafeMenu;
 
        public Waitress(IMenu pancakeHouseMenu, IMenu dinerMenu, IMenu cafeMenu) 
        {
            this.pancakeHouseMenu = pancakeHouseMenu;
            this.dinerMenu = dinerMenu;
            this.cafeMenu = cafeMenu;
        }
 
        public void PrintMenu() 
        {
            IEnumerator pancakeIterator = pancakeHouseMenu.CreateIterator();
            IEnumerator dinerIterator = dinerMenu.CreateIterator();
            IEnumerator cafeIterator = cafeMenu.CreateIterator();

            Console.WriteLine("MENU\n----\nBREAKFAST");
            PrintMenu(pancakeIterator);

            Console.WriteLine("\nLUNCH");
            PrintMenu(dinerIterator);

            Console.WriteLine("\nDINNER");
            PrintMenu(cafeIterator);
        }
 
        private void PrintMenu(IEnumerator iterator) 
        {
            while (iterator.MoveNext()) 
            {
                MenuItem menuItem = (MenuItem)iterator.Current;

                Console.Write(menuItem.Name + ", ");
                Console.Write(menuItem.Price + " -- ");
                Console.WriteLine(menuItem.Description);
            }
        }
 
        public void PrintVegetarianMenu() 
        {
            Console.WriteLine("\nVEGETARIAN MENU\n---------------");
            PrintVegetarianMenu(pancakeHouseMenu.CreateIterator());
            PrintVegetarianMenu(dinerMenu.CreateIterator());
            PrintVegetarianMenu(cafeMenu.CreateIterator());
        }
 
        public bool IsItemVegetarian(string name) 
        {
            IEnumerator pancakeIterator = pancakeHouseMenu.CreateIterator();
            if (IsVegetarian(name, pancakeIterator)) 
            {
                return true;
            }
            IEnumerator dinerIterator = dinerMenu.CreateIterator();
            if (IsVegetarian(name, dinerIterator)) 
            {
                return true;
            }
            IEnumerator cafeIterator = cafeMenu.CreateIterator();
            if (IsVegetarian(name, cafeIterator)) 
            {
                return true;
            }
            return false;
        }

        private void PrintVegetarianMenu(IEnumerator iterator) 
        {
            while (iterator.MoveNext()) 
            {
                MenuItem menuItem = (MenuItem)iterator.Current;
                if (menuItem.Vegetarion) 
                {
                    Console.Write(menuItem.Name + ", ");
                    Console.Write(menuItem.Price + " -- ");
                    Console.WriteLine(menuItem.Description);
                }
            }
        }

        private bool IsVegetarian(string name, IEnumerator iterator) 
        {
            while (iterator.MoveNext()) 
            {
                MenuItem menuItem = (MenuItem)iterator.Current;
                if (menuItem.Name == name) 
                {
                    if (menuItem.Vegetarion) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    #endregion

    #region Menu and MenuItems

    public interface IMenu 
    {
        IEnumerator CreateIterator();
    }

    public class MenuItem 
    {
        String name;
        String description;
        bool vegetarian;
        double price;
 
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
  
        public string Name 
        {
            get{ return name; }
        }
  
        public string Description
        {
            get{ return description; }
        }
  
        public double Price
        {
            get{ return price; }
        }
  
        public bool Vegetarion
        {
            get{ return vegetarian; }
        }
    }

    public class PancakeHouseMenu : IMenu 
    {
        ArrayList menuItems;
 
        public PancakeHouseMenu() 
        {
            menuItems = new ArrayList();
    
            AddItem("K&B's Pancake Breakfast", 
                "Pancakes with scrambled eggs, and toast", 
                true,
                2.99);
 
            AddItem("Regular Pancake Breakfast", 
                "Pancakes with fried eggs, sausage", 
                false,
                2.99);
 
            AddItem("Blueberry Pancakes",
                "Pancakes made with fresh blueberries, and blueberry syrup",
                true,
                3.49);
 
            AddItem("Waffles",
                "Waffles, with your choice of blueberries or strawberries",
                true,
                3.59);
        }

        public void AddItem(string name, string description, bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            menuItems.Add(menuItem);
        }
 
        public IEnumerator CreateIterator() 
        {
            return menuItems.GetEnumerator();
        }
  
        // other menu methods here
    }

    public class CafeMenu : IMenu 
    {
        Hashtable menuItems = new Hashtable();
  
        public CafeMenu() 
        {
            AddItem("Veggie Burger and Air Fries",
                "Veggie burger on a whole wheat bun, lettuce, tomato, and fries",
                true, 3.99);
            AddItem("Soup of the day",
                "A cup of the soup of the day, with a side salad",
                false, 3.69);
            AddItem("Burrito",
                "A large burrito, with whole pinto beans, salsa, guacamole",
                true, 4.29);
        }
 
        public void AddItem(String name, String description, 
            bool vegetarian, double price) 
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            menuItems.Add(menuItem.Name, menuItem);
        }
 
        public IEnumerator CreateIterator() 
        {
            return menuItems.Values.GetEnumerator();
        }
    }

    public class DinerMenu : IMenu 
    {
        static readonly int MAX_ITEMS = 6;
        int numberOfItems = 0;
        MenuItem[] menuItems;
  
        public DinerMenu() 
        {
            menuItems = new MenuItem[MAX_ITEMS];
 
            AddItem("Vegetarian BLT",
                "(Fakin') Bacon with lettuce & tomato on whole wheat", true, 2.99);
            AddItem("BLT",
                "Bacon with lettuce & tomato on whole wheat", false, 2.99);
            AddItem("Soup of the day",
                "Soup of the day, with a side of potato salad", false, 3.29);
            AddItem("Hotdog",
                "A hot dog, with saurkraut, relish, onions, topped with cheese",
                false, 3.05);
            AddItem("Steamed Veggies and Brown Rice",
                "A medly of steamed vegetables over brown rice", true, 3.99);
            AddItem("Pasta",
                "Spaghetti with Marinara Sauce, and a slice of sourdough bread",
                true, 3.89);
        }
  
        public void AddItem(String name, String description, 
            bool vegetarian, double price) 
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            if (numberOfItems >= MAX_ITEMS) 
            {
                Console.WriteLine("Sorry, menu is full!  Can't add item to menu");
            } 
            else 
            {
                menuItems[numberOfItems] = menuItem;
                numberOfItems = numberOfItems + 1;
            }
        }
 
        public IEnumerator CreateIterator() 
        {
            return menuItems.GetEnumerator();
        }
 
        // other menu methods here
    }

    #endregion
}
