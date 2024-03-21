﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepositoryPattern.Models
{
    public class Order
    {
        public int Id
        {
            get;
            set;
        }

        [Required]
        public DateTime Date
        {
            get;
            set;
        }

        [Required]
        public Customer Customer
        {
            get;
            set;
        }

        [Required]
        public ICollection<OrderLine> Lines
        {
            get;
            set;
        }
    }
}
