using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.DataLayer.DataObjects
{
    /// <summary>
    /// Abstract factory class that creates data access objects.
    /// 
    /// GoF Design Pattern: Factory.
    /// </summary>
    public abstract class DaoFactory
    {
        /// <summary>
        /// Gets a customer data access object.
        /// </summary>
        public abstract ICustomerDao CustomerDao { get; }

        /// <summary>
        /// Gets an order data access object.
        /// </summary>
        public abstract IOrderDao OrderDao { get; }

        /// <summary>
        /// Gets a product data access object.
        /// </summary>
        public abstract IProductDao ProductDao { get; }
    }
}
