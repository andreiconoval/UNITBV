using System;
using System.Collections;

namespace DoFactory.HeadFirst.Iterator.DinerMergerI
{
    class MenuTestDrive
    {
        static void Main(string[] args)
        {
            PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();

            Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu);
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
        Menu pancakeHouseMenu;
        Menu dinerMenu;
 
        public Waitress(Menu pancakeHouseMenu, Menu dinerMenu) 
        {
            this.pancakeHouseMenu = pancakeHouseMenu;
            this.dinerMenu = dinerMenu;
        }
 
        public void PrintMenu() 
        {
            IEnumerator pancakeIterator = pancakeHouseMenu.CreateIterator();
            IEnumerator dinerIterator = dinerMenu.CreateIterator();

            Console.WriteLine("MENU\n----\nBREAKFAST");
            PrintMenu(pancakeIterator);

            Console.WriteLine("\nLUNCH");
            PrintMenu(dinerIterator);
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
            Console.WriteLine("\nVEGETARIAN MENU\n----\nBREAKFAST");
            PrintVegetarianMenu(pancakeHouseMenu.CreateIterator());
            Console.WriteLine("\nLUNCH");
            PrintVegetarianMenu(dinerMenu.CreateIterator());
        }
 
        public bool IsItemVegetarian(String name) 
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
            return false;
        }

        private void PrintVegetarianMenu(IEnumerator iterator) 
        {
            while (iterator.MoveNext()) 
            {
                MenuItem menuItem = (MenuItem)iterator.Current;
                if (menuItem.Vegetarian) 
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
                if (menuItem.Name.Equals(name)) 
                {
                    if (menuItem.Vegetarian) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    #endregion

    #region Iterators (Enumerators)

    public class AlternatingDinerMenuIterator : IEnumerator 
    {
        MenuItem[] items;
        int position;

        public AlternatingDinerMenuIterator(MenuItem[] items) 
        {
            this.items = items;
            position = int.Parse(DateTime.Now.DayOfWeek.ToString()) % 2;
        }

        public object Current
        {
            get
            {
                MenuItem menuItem = items[position];
                position = position + 2;
                return menuItem;
            }
        }

        public bool MoveNext() 
        {
            if (position >= items.Length || items[position] == null) 
            {
                return false;
            } 
            else 
            {
                return true;
            }
        }

        public void Reset()
        {
            Console.WriteLine("Alternating Diner Menu Iterator does not support reset()");
        }
    }

    public class DinerMenuIterator : IEnumerator 
    {
        MenuItem[] list;
        int position = 0;
 
        public DinerMenuIterator(MenuItem[] list) 
        {
            this.list = list;
        }
 
        public object Current
        {
            get
            {

                MenuItem menuItem = list[position];
                position = position + 1;
                return menuItem;
            }
        }
 
        public bool MoveNext() 
        {
            if (position >= list.Length || list[position] == null) 
            {
                return false;
            } 
            else 
            {
                return true;
            }
        }
  
        public void Reset()
        {
            if (position <= 0) 
            {
                throw new ApplicationException
                    ("You can't remove an item until you've done at least one Next()");
            }
            if (list[position-1] != null) 
            {
                for (int i = position-1; i < (list.Length-1); i++) 
                {
                    list[i] = list[i+1];
                }
                list[list.Length-1] = null;
            }
        }
    }

    #endregion 

    #region Menu and MenuItems

    public interface Menu 
    {
        IEnumerator CreateIterator();
    }

    public class MenuItem 
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
  
        // Properties
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
  
        public bool Vegetarian 
        {
            get{ return vegetarian; }
        }
    }

    public class DinerMenu : Menu 
    {
        static int MAX_ITEMS = 6;
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
                "Steamed vegetables over brown rice", true, 3.99);
            AddItem("Pasta",
                "Spaghetti with Marinara Sauce, and a slice of sourdough bread",
                true, 3.89);
        }
  
        public void AddItem(string name, string description, bool vegetarian, double price) 
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
 
        public MenuItem[] getMenuItems() 
        {
            return menuItems;
        }
  
        public IEnumerator CreateIterator() 
        {
            return new DinerMenuIterator(menuItems);
        }
 
        // other menu methods here
    }


    public class PancakeHouseMenu : Menu 
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

        public void AddItem(String name, string description,bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            menuItems.Add(menuItem);
        }
 
        public ArrayList getMenuItems() 
        {
            return menuItems;
        }
  
        public IEnumerator CreateIterator() 
        {
            return menuItems.GetEnumerator();
        }
  
        // other menu methods here
    }

    #endregion
}
