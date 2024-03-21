using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsSOAClient
{
    /// <summary>
    /// Order detail business object as defined on SOA client side.
    /// </summary>
    class OrderDetail
    {
        private string _product;
        private int _quantity;
        private float _unitPrice;
        private float _discount;
        private Order _order; // The parent

        /// <summary>
        /// Gets or sets product name.
        /// </summary>
        public string Product
        {
            get { return _product; }
            set { _product = value; }
        }

        /// <summary>
        /// Gets or sets quantity of products ordered.
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /// <summary>
        /// Gets or set unit price of product.
        /// </summary>
        public float UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        /// <summary>
        /// Gets or sets discount applied to unit price in this order.
        /// </summary>
        public float Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        /// <summary>
        /// Gets or sets order of which this order detail is a part.
        /// </summary>
        public Order Order
        {
            get { return _order; }
            set { _order = value; }
        }
    }
}
