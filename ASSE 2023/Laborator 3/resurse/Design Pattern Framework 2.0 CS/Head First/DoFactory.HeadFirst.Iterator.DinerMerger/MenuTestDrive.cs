using System;
using System.Collections;

namespace DoFactory.HeadFirst.Iterator.DinerMerger
{
    class MenuTestDrive
    {
        static void Main(string[] args)
        {
            PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();
 
            Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu);
 
            waitress.PrintMenu();

            // Wait for user
            Console.Read();
        }
    }

    #region Waitress

    public class Waitress 
    {
        PancakeHouseMenu pancakeHouseMenu;
        DinerMenu dinerMenu;
 
        public Waitress(PancakeHouseMenu pancakeHouseMenu, DinerMenu dinerMenu) 
        {
            this.pancakeHouseMenu = pancakeHouseMenu;
            this.dinerMenu = dinerMenu;
        }
 
        public void PrintMenu() 
        {
            Iterator pancakeIterator = pancakeHouseMenu.CreateIterator();
            Iterator dinerIterator = dinerMenu.CreateIterator();

            Console.WriteLine("MENU\n----\nBREAKFAST");
            PrintMenu(pancakeIterator);

            Console.WriteLine("\nLUNCH");
            PrintMenu(dinerIterator);
        }
 
        private void PrintMenu(Iterator iterator) 
        {
            while (iterator.HasNext()) 
            {
                MenuItem menuItem = (MenuItem)iterator.Next();
                Console.Write(menuItem.getName() + ", ");
                Console.Write(menuItem.getPrice() + " -- ");
                Console.WriteLine(menuItem.getDescription());
            }
        }
 
        public void PrintVegetarianMenu() 
        {
            PrintVegetarianMenu(pancakeHouseMenu.CreateIterator());
            PrintVegetarianMenu(dinerMenu.CreateIterator());
        }
 
        public bool IsItemVegetarian(String name) 
        {
            Iterator breakfastIterator = pancakeHouseMenu.CreateIterator();
            if (IsVegetarian(name, breakfastIterator)) 
            {
                return true;
            }

            Iterator dinnerIterator = dinerMenu.CreateIterator();
            if (IsVegetarian(name, dinnerIterator)) 
            {
                return true;
            }
            return false;
        }

        private void PrintVegetarianMenu(Iterator iterator) 
        {
            while (iterator.HasNext()) 
            {
                MenuItem menuItem = (MenuItem)iterator.Next();
                if (menuItem.IsVegetarian()) 
                {
                    Console.WriteLine(menuItem.getName());
                    Console.WriteLine("\t\t" + menuItem.getPrice());
                    Console.WriteLine("\t" + menuItem.getDescription());
                }
            }
        }

        private bool IsVegetarian(string name, Iterator iterator) 
        {
            while (iterator.HasNext()) 
            {
                MenuItem menuItem = (MenuItem)iterator.Next();
                if (menuItem.getName().Equals(name)) 
                {
                    if (menuItem.IsVegetarian()) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    #endregion

    #region Iterators

    public interface Iterator 
    {
        bool HasNext();
        object Next();
    }

    public class AlternatingDinerMenuIterator : Iterator 
    {
        MenuItem[] list;
        int position;

        public AlternatingDinerMenuIterator(MenuItem[] list) 
        {
            this.list = list;
            position = int.Parse(DateTime.Now.DayOfWeek.ToString()) % 2;
        }

        public object Next() 
        {
            MenuItem menuItem = list[position];
            position = position + 2;
            return menuItem;
        }

        public bool HasNext() 
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

        public override string ToString() 
        {
            return "Alternating Diner Menu Iterator";
        }
    }

    public class ArrayIterator : Iterator 
    {
        MenuItem[] items;
        int position = 0;
 
        // Constructore
        public ArrayIterator(MenuItem[] items) 
        {
            this.items = items;
        }
 
        public object Next() 
        {
            MenuItem menuItem = items[position];
            position = position + 1;
            return menuItem;
        }
 
        public bool HasNext() 
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
    }

    public class ArrayListIterator : Iterator 
    {
        ArrayList items;
        int position = 0;
 
        // Constructor
        public ArrayListIterator(ArrayList items) 
        {
            this.items = items;
        }
 
        public object Next() 
        {
            object o = items[position];
            position = position + 1;
            return o;
        }
 
        public bool HasNext() 
        {
            if (position >= items.Count) 
            {
                return false;
            } 
            else 
            {
                return true;
            }
        }
    }

    public class DinerMenuIterator : Iterator 
    {
        MenuItem[] items;
        int position = 0;
 
        public DinerMenuIterator(MenuItem[] items) 
        {
            this.items = items;
        }
 
        public Object Next() 
        {
            MenuItem menuItem = items[position];
            position = position + 1;
            return menuItem;
        }
 
        public bool HasNext() 
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
    }
    #endregion

    #region Menu and MenuItems

    public interface Menu 
    {
        Iterator CreateIterator();
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
  
        public String getName() 
        {
            return name;
        }
  
        public String getDescription() 
        {
            return description;
        }
  
        public double getPrice() 
        {
            return price;
        }
  
        public bool IsVegetarian() 
        {
            return vegetarian;
        }

        public override string ToString() 
        {
            return (name + ", $" + price + "\n   " + description);
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
 
        public MenuItem[] GetMenuItems() 
        {
            return menuItems;
        }
  
        public Iterator CreateIterator() 
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
                "Pancakes made with fresh blueberries",
                true,
                3.49);
 
            AddItem("Waffles",
                "Waffles, with your choice of blueberries or strawberries",
                true,
                3.59);
        }

        public void AddItem(String name, String description,
            bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            menuItems.Add(menuItem);
        }
 
        public ArrayList GetMenuItems() 
        {
            return menuItems;
        }
  
        public Iterator CreateIterator() 
        {
            return new PancakeHouseMenuIterator(menuItems);
        }
  
        public override string ToString() 
        {
            return "Objectville Pancake House Menu";
        }

        // other menu methods here
    }

    public class PancakeHouseMenuIterator : Iterator 
    {
        ArrayList items;
        int position = 0;
 
        public PancakeHouseMenuIterator(ArrayList items) 
        {
            this.items = items;
        }
 
        public object Next() 
        {
            object o = items[position];
            position = position + 1;
            return o;
        }
 
        public bool HasNext() 
        {
            if (position >= items.Count)
            {
                return false;
            } 
            else 
            {
                return true;
            }
        }
    }
    #endregion
}
