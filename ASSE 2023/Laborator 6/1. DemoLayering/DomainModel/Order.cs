﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF6CodeFirst.DomainModel
{
    public partial class Order
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
