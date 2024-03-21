﻿using System.ComponentModel.DataAnnotations;

namespace RepositoryPattern.Models
{
    public class OrderLine
    {
        public int Id { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public Order Order
        {
            get;
            set;
        }

        public decimal Price
        {
            get
            {
                return Product.UnitPrice * (decimal)Quantity;
            }
        }
    }
}
