using System;
using System.Collections.Generic;
using System.Text;

using DoFactory.BusinessLayer.BusinessObjects;
using DoFactory.DataLayer.DataObjects;

namespace DoFactory.BusinessLayer.Facade.ProxyObjects
{
    /// <summary>
    /// A proxy object representing a list of Order Details. The ProxyList abstract base 
    /// class implements lazy loading (also called just-in-time loading).  Therefore 
    /// the Order Detail list is loaded only when it is absolutely necessary.  This class
    /// is a proxy to a limited resource (the list coming from the database). 
    /// 
    /// GoF Design Patterns: Proxy.
    /// Enterprise Design Patterns: LazyLoad.
    /// </summary>
    [Serializable]
    public class ProxyForOrderDetails<OrderDetail> : ProxyList<OrderDetail>
    {
        private IOrderDao orderDao = DataAccess.OrderDao;
        private Order _order;

        /// <summary>
        /// Constructor for ProxyForOrderDetails.
        /// </summary>
        /// <param name="order">Order.</param>
        public ProxyForOrderDetails(Order order)
        {
            _order = order;
        }

        /// <summary>
        /// Loads the order details associated with an order.
        /// </summary>
        /// <returns>Number of items loaded.</returns>
        protected override int LoadList()
        {
            base.List = (IList<OrderDetail>)orderDao.GetOrderDetails(_order);
            return Count;
        }
    }
}
