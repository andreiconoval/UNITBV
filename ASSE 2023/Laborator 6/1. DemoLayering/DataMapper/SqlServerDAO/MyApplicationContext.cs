using DemoEF6CodeFirst.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    class MyApplicationContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="MyApplicationContext" /> class.</summary>
        public MyApplicationContext() : base("myConStr")
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
