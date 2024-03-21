using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about an order.
    /// 
    /// Enterprise Design Pattern: Domain Model, Identity Field, Foreign Key Mapping.
    /// </summary>
    /// <remarks>
    /// This is where your business logic resides. In this example there are none.
    /// Another place for business logic is in the Facade.  
    /// For an example see CustomerFacade in the Facade layer.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// 
    /// The Identity Field Design Pattern saves the ID field in an object to maintain
    /// identity between an in-memory business object and that database rows.
    /// 
    /// The Foreign Key Mapping Design Pattern is implemented by the Order to Customer 
    /// reference. The pattern states that it maps an association between objects to 
    /// a foreign key reference between table. The CustomerId is the foreign key to the 
    /// Order. 
    /// </remarks>
    [Serializable]
    public class Order
    {
        // Enterprise Design Pattern: Identity field
        private int _orderId;

        private DateTime _orderDate;
        private DateTime _requiredDate;
        private float _freight;
        private IList<OrderDetail> _orderDetails;

        // Enterprise Design Pattern: Foreign Key Mapping
        private Customer _customer;  // The parent

        /// <summary>
        /// Default constructor for order class.
        /// </summary>
        public Order() { }

        /// <summary>
        /// Overloaded constructor for the order class.
        /// </summary>
        /// <param name="orderId">Unique identifier for the Order</param>
        /// <param name="orderDate">Date at which Order is placed.</param>
        /// <param name="requiredDate">Date at which Order is required.</param>
        /// <param name="freight">Freight (shipping) costs for the Order.</param>
        public Order(int orderId, DateTime orderDate, DateTime requiredDate, float freight)
        {
            this._orderId = orderId;
            this._orderDate = orderDate;
            this._requiredDate = requiredDate;
            this._freight = freight;
        }

        /// <summary>
        /// Gets or sets unique identifier for the order.
        /// The Identity Field Design Pattern. 
        /// </summary>
        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        /// <summary>
        /// Gets or sets the date at which the order is placed.
        /// </summary>
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        /// <summary>
        /// Gets or sets the date at which delivery of the order is required.
        /// </summary>
        public DateTime RequiredDate
        {
            get { return _requiredDate; }
            set { _requiredDate = value; }
        }

        /// <summary>
        /// Gets or sets the freight (shipping) costs for this order.
        /// </summary>
        public float Freight
        {
            get { return _freight; }
            set { _freight = value; }
        }

        /// <summary>
        /// Gets or sets the customer associated with the order.
        /// </summary>
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        /// <summary>
        /// Gets the customer name associated with the order.
        /// </summary>
        public string CustomerName
        {
            get { return Customer.Company; }
        }

        /// <summary>
        /// Gets or sets a list of order details (line items) for the order.
        /// </summary>
        public IList<OrderDetail> OrderDetails
        {
            get { return _orderDetails; }
            set { _orderDetails = value; }
        }
    }
}
