using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF6CodeFirst.DataMapper
{
    public class Product
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id{ get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "The name cannot be null", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength =3, ErrorMessage = "The product name length must be between 3 and 100 chars")]
        public string Name { get; set; }

        /// <summary>Gets or sets the measurement unit.</summary>
        /// <value>The measurement unit.</value>
        [Required(ErrorMessage = "The measurement unit cannot be null", AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "The measurement unit length must be between 1 and 20 chars")]
        public string MeasurementUnit { get; set; }

        /// <summary>Gets or sets the unit price.</summary>
        /// <value>The unit price.</value>
        [Required(ErrorMessage = "The unit price cannot be null")]
        public decimal UnitPrice{ get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        [Required(ErrorMessage = "Each product is part of a category")]
        public virtual Category Category { get; set; }
    }
}
