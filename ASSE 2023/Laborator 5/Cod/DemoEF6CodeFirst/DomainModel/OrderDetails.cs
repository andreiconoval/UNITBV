using System.ComponentModel.DataAnnotations;

namespace DemoEF6CodeFirst.DataMapper
{
    public class OrderDetails
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the order.</summary>
        /// <value>The order.</value>
        [Required(ErrorMessage ="An order detail must belong to an Order object")]
        public Order Order { get; set; }

        [Required(ErrorMessage ="A product must be specified")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "The quantity must be soecified")]
        [Range(0, double.PositiveInfinity, ErrorMessage ="Positive quantity requested")]
        public double Quantity { get; set; }

        public decimal Price
        {
            get
            {
                return Product.UnitPrice * (decimal)this.Quantity;
            }
        }
    }
}