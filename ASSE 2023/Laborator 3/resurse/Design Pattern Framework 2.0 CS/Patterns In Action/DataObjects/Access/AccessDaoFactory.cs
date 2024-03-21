using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.DataLayer.DataObjects.Access
{
    /// <summary>
    /// Microsoft Access specific factory that creates Microsoft Access 
    /// specific data access objects.
    /// 
    /// GoF Design Patterns: Factory.
    /// </summary>
    public class AccessDaoFactory : DaoFactory
    {
        /// <summary>
        /// Gets a Microsoft Access specific customer data access object.
        /// </summary>
        public override ICustomerDao CustomerDao
        {
            get { return new AccessCustomerDao(); }
        }

        /// <summary>
        /// Gets a Microsoft Access specific order data access object.
        /// </summary>
        public override IOrderDao OrderDao
        {
            get { return new AccessOrderDao(); }
        }

        /// <summary>
        /// Gets a Microsoft Access specific product data access object.
        /// </summary>
        public override IProductDao ProductDao
        {
            get { return new AccessProductDao(); }
        }
    }
}
