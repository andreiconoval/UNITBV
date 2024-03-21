using System;
using System.Collections.Generic;
using System.Text;

using DoFactory.BusinessLayer.BusinessObjects;
using DoFactory.DataLayer.DataObjects;

namespace DoFactory.BusinessLayer.Facade.ProxyObjects
{
    /// <summary>
    /// A proxy object representing a list of Orders. The ProxyList abstract base 
    /// class implements lazy loading (also called just-in-time loading).  Therefore 
    /// the Order list is loaded only when it is absolutely necessary.  This class
    /// is a proxy to a limited resource (the list coming from the database). 
    /// 
    /// GoF Design Patterns: Proxy.
    /// Enterprise Design Patterns: LazyLoad.
    /// </summary>

    [Serializable]
    public class ProxyForOrders<Order> : ProxyList<Order>
    {
        private IOrderDao orderDao = DataAccess.OrderDao;
        private Customer _customer;

        /// <summary>
        /// Constructor for ProxyForOrders.
        /// </summary>
        /// <param name="customer">Customer.</param>
        public ProxyForOrders(Customer customer)
        {
            _customer = customer;
        }

        /// <summary>
        /// Loads the orders associated with a customer.
        /// </summary>
        /// <returns>Number of items loaded.</returns>
        protected override int LoadList()
        {
            base.List = (IList<Order>)orderDao.GetOrders(_customer);
            return Count;
        }
    }
}
