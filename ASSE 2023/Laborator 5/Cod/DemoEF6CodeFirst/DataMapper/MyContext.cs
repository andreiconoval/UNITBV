using DemoEF6CodeFirst.DataMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF6CodeFirst.DomainModel
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyConnectionString")
        {

        }

        public virtual DbSet<Product> Products
        {
            get;
            set;
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
