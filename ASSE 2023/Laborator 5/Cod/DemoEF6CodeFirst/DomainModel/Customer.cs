using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF6CodeFirst.DataMapper
{
    public class Customer
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id{ get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "The customer name has to be specified", AllowEmptyStrings =false)]
        [StringLength(maximumLength:50, ErrorMessage = "At most 50 chars")]
        public string Name{ get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        [StringLength(maximumLength:300, ErrorMessage = "At most 300 chars for address")]
        public string Address { get; set; }
    }
}
