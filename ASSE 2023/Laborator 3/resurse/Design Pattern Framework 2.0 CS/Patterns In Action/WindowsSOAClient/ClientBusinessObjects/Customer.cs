using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsSOAClient
{
    /// <summary>
    /// Customer business object as seen by the Web Service client.
    /// </summary>
    /// <remarks>
    /// The client's view of what a business object is may be different
    /// from how the Patterns In Action Business Layer sees it. For one thing, 
    /// the identifiers are encrypted on this side.  However, the encryption 
    /// is totally irrelevant to the client. In fact, they don't even have to 
    /// know it is encrypted.
    /// </remarks>
    class Customer
    {
        private string _customerId;
        private string _company;
        private string _city;
        private string _country;
        private IList<Order> _orders;

        /// <summary>
        /// Gets or sets customerId. This id is encrypted.
        /// </summary>
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        /// <summary>
        /// Gets or sets customer name.
        /// </summary>
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        /// <summary>
        /// Gets or sets customer city.
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        /// Gets or set customer country.
        /// </summary>
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        /// <summary>
        /// Gets or sets list of orders associated with customer.
        /// </summary>
        public IList<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }
    }
}
