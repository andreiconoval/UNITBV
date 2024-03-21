using DemoEF6CodeFirst.DataMapper;
using DemoEF6CodeFirst.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF6CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            addProduct();

            //listAllProducts();

            //addOrder();
        }

        private static void addOrder()
        {
            using(var context = new MyContext())
            {
                var customer = new Customer
                {
                    Name = "Popescu",
                    Address = "Brasov"
                };

                try
                {
                    var product = context.Products.Where(p => p.Name == "Paine").SingleOrDefault();
                    if (product != null)
                    {

                        Order order = new Order
                        {
                            Customer = customer,
                            OrderDate = DateTime.Now,
                            OrderDetails = new HashSet<OrderDetails> { new OrderDetails { Product = product, Quantity = 3 } }
                        };

                        context.Orders.Add(order);
                        context.SaveChanges();
                    }
                }
                catch(Exception exception)
                {
                    Console.WriteLine($"Exception: {exception.Message}");
                }
            }
        }

        private static void listAllProducts()
        {
            using(var context = new MyContext())
            {
                var products = context.Products.Include("Category").OrderBy(product => product.Name).ToList();

                foreach (var item in products)
                {
                    Console.WriteLine($"Name: {item.Name}, category: {item.Category.Name}, unit price: {item.UnitPrice}, measurement unit: {item.MeasurementUnit}");
                }
            }
        }

        private static void addProduct()
        {
            using(var context = new MyContext())
            {
                Product product = new Product
                {
                    Name = "Paine",
                    MeasurementUnit = "Bucata",
                    UnitPrice = 3.5m,
                    Category = new Category
                    {
                        Name = "Produse alimentare",
                        Description = "Produse alimentare de consum uman",
                    }
                };

                context.Products.Add(product);
                context.SaveChanges();
            }
        }
    }
}
