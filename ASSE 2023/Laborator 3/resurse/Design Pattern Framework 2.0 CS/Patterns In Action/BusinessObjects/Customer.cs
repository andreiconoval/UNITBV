using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.BusinessLayer.BusinessObjects
{   
    /// <summary>
    /// Class that holds information about a customer.
    /// 
    /// Enterprise Design Pattern: Domain Model, Identity Field.
    /// </summary>
    /// <remarks>
    /// This is where your business logic resides. In this example there are none.
    /// Another place for business logic and business rules is in the Facade.  
    /// For an example see CustomerFacade in the Facade layer.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// 
    /// The Identity Field Design Pattern saves the ID field in an object to maintain
    /// identity between an in-memory business object and that database rows.
    /// </remarks>
    [Serializable]
    public class Customer
    {
        private int _customerId;
        private string _company;
        private string _city;
        private string _country;

        // Order statistics
        private int _numOrders = 0;
        private DateTime _lastOrderDate;

        // List of orders
        private IList<Order> _orders;

        /// <summary>
        /// Default constructor for customer class.
        /// </summary>
        public Customer() { }

        /// <summary>
        /// Overloaded constructor for the Customer class.
        /// </summary>
        /// <param name="customerId">Unique Identifier for the Customer.</param>
        /// <param name="company">Name of the Customer.</param>
        /// <param name="city">City where Customer is located.</param>
        /// <param name="country">Country where Customer is located.</param>
        public Customer(int customerId, string company, string city, string country)
        {
            this._customerId = customerId;
            this._company = company;
            this._city = city;
            this._country = country;
        }

        /// <summary>
        /// Gets or sets unique customer identifier.
        /// The Identity Field Design Pattern. 
        /// </summary>
        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        /// <summary>
        /// Gets or sets the customer city.
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        /// Gets or sets the customer country.
        /// </summary>
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        /// <summary>
        /// Gets or sets the total number of orders placed by the customer.
        /// </summary>
        public int NumOrders
        {
            get { return _numOrders; }
            set { _numOrders = value; }
        }

        /// <summary>
        /// Gets or sets the last date the customer placed an order.
        /// </summary>
        public DateTime LastOrderDate
        {
            get { return _lastOrderDate; }
            set { _lastOrderDate = value; }
        }

        /// <summary>
        /// Gets or sets a list of all orders placed by the customer.
        /// </summary>
        public IList<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }
    }
}
