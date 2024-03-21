using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsSOAClient
{
    /// <summary>
    /// Order business object as seen by the Web Service client.
    /// </summary>
    /// <remarks>
    /// The client's view of what a business object is may be different
    /// from how the Patterns In Action Business Layer sees it. For one thing, 
    /// the identifiers are encrypted on this side.  However, the encryption 
    /// is totally irrelevant to the client. In fact, they don't even have to 
    /// know it is encrypted.
    /// </remarks>
    class Order
    {
        private string _orderId;
        private DateTime _orderDate;
        private DateTime _requiredDate;
        private float _freight;
        private IList<OrderDetail> _orderDetails;
        private Customer _customer;   // The parent

        /// <summary>
        /// Gets or sets order identifier. This identifier is encrypted.
        /// </summary>
        public string OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        /// <summary>
        /// Gets or sets order date.
        /// </summary>
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        /// <summary>
        /// Gets or set required order delivery date.
        /// </summary>
        public DateTime RequiredDate
        {
            get { return _requiredDate; }
            set { _requiredDate = value; }
        }

        /// <summary>
        /// Gets or sets freight (shipping) costs.
        /// </summary>
        public float Freight
        {
            get { return _freight; }
            set { _freight = value; }
        }

        /// <summary>
        /// Gets or sets list of order details (line items) for order.
        /// </summary>
        public IList<OrderDetail> OrderDetails
        {
            get { return _orderDetails; }
            set { _orderDetails = value; }
        }

        /// <summary>
        /// Gets or sets customer for which order is placed.
        /// </summary>
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }
    }
}
