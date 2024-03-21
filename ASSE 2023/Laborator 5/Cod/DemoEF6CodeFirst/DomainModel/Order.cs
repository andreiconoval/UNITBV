using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF6CodeFirst.DataMapper
{
    public class Order
    {
        /// <summary>Initializes a new instance of the <see cref="Order" /> class.</summary>
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the customer.</summary>
        /// <value>The customer.</value>
        [Required(ErrorMessage = "The customer must be specified")]
        public Customer Customer { get; set; }

        /// <summary>Gets or sets the order date.</summary>
        /// <value>The order date.</value>
        [Required(ErrorMessage = "The Order's date must be specified")]
        public DateTime OrderDate { get; set; }

        public HashSet<OrderDetails> OrderDetails
        {
            get;
            set;
        }
    }
}
