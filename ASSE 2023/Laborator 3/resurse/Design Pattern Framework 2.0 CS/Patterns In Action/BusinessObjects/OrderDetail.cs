using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds order details (line items) for an order.
    /// 
    /// Enterprise Design Pattern: Domain Model.
    /// </summary>
    /// <remarks>
    /// This is where your business logic resides. In this example there are none.
    /// Another place for business logic and business rules is in the Facade.  
    /// For an example see CustomerFacade in the Facade layer.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// </remarks>
    [Serializable]
    public class OrderDetail
    {
        private string _product;
        private int _quantity;
        private float _unitPrice;
        private float _discount;
        private Order _order;

        /// <summary>
        /// Default constructor for Order Detail.
        /// </summary>
        public OrderDetail() { }

        /// <summary>
        /// Overloaded  constructor for Order Detail.
        /// </summary>
        /// <param name="product">Product name of Order Detail.</param>
        /// <param name="quantity">Quantity ordered.</param>
        /// <param name="unitPrice">Unit price of product at the time order is placed.</param>
        /// <param name="discount">Discount applied to unit price of product.</param>
        /// <param name="order">Order that Order Detail is part of.</param>
        public OrderDetail(string product, int quantity, float unitPrice, float discount, Order order)
        {
            this._product = product;
            this._quantity = quantity;
            this._unitPrice = unitPrice;
            this._discount = discount;
            this._order = order; // The parent object
        }

        /// <summary>
        /// Get or set Product name of Order Detail (line item).
        /// </summary>
        public string Product
        {
            get { return _product; }
            set { _product = value; }
        }

        /// <summary>
        /// Get or set quantity of Products ordered.
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /// <summary>
        /// Get or set unit price of Product in US$.
        /// </summary>
        public float UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        /// <summary>
        /// Get or set discount applied to unit price.
        /// </summary>
        public float Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        /// <summary>
        /// Get or set the Order of which Order Detail is part of.
        /// </summary>
        public Order Order
        {
            get { return _order; }
            set { _order = value; }
        }
    }
}
