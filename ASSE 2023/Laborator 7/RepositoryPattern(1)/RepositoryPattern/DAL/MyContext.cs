using RepositoryPattern.Models;
using System.Data.Entity;

namespace RepositoryPattern.DataMapper
{
    class MyContext : DbContext
    {
        public MyContext()
            : base("myConStr")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
