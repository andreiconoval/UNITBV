using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using DoFactory.BusinessLayer.BusinessObjects;

namespace DoFactory.DataLayer.DataObjects.SqlServer
{
    /// <summary>
    /// Sql Server specific data access object that handles data access
    /// of customer related orders and order details.
    /// </summary>
    public class SqlServerOrderDao : IOrderDao
    {
        /// <summary>
        /// Gets customers with order statistics in given sort order.
        /// </summary>
        /// <param name="customers">Customer list.</param>
        /// <returns>Sorted list of customers with order statistics.</returns>
        public IList<Customer> GetOrderStatistics(IList<Customer> customers)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Customer customer in customers)
            {
                sb.Append(customer.CustomerId);
                sb.Append(",");
            }
            sb = sb.Remove(sb.Length - 1, 1);

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CustomerId, MAX(OrderDate) AS LastOrderDate, COUNT(OrderId) AS NumOrders ");
            sql.Append("   FROM [Order]");
            sql.Append("  WHERE CustomerId IN (" + sb.ToString() + ")");
            sql.Append("  GROUP BY CustomerId ");

            DataTable dt = Db.GetDataTable(sql.ToString());

            // Loop over customers first to preserve sort order
            foreach (Customer customer in customers)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (int.Parse(row["CustomerId"].ToString()) == customer.CustomerId)
                    {
                        customer.NumOrders = int.Parse(row["NumOrders"].ToString());
                        customer.LastOrderDate = DateTime.Parse(row["LastOrderDate"].ToString());

                        break;
                    }
                }
            }

            return customers;
        }

        /// <summary>
        /// Gets a list of customers with order summary statistics.
        /// </summary>
        /// <param name="customers">Customer list.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Customer list with order summary statistics.</returns>
        public IList<Customer> GetOrderStatistics(IList<Customer> customers, string sortExpression)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Customer customer in customers)
            {
                sb.Append(customer.CustomerId);
                sb.Append(",");
            }
            sb = sb.Remove(sb.Length - 1, 1);

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CustomerId, MAX(OrderDate) AS LastOrderDate, COUNT(OrderId) AS NumOrders ");
            sql.Append("   FROM [Order]");
            sql.Append("  WHERE CustomerId IN (" + sb.ToString() + ")");
            sql.Append("  GROUP BY CustomerId ");

            if (!String.IsNullOrEmpty(sortExpression))
                sql.Append("  ORDER BY " + sortExpression);


            DataTable dt = Db.GetDataTable(sql.ToString());

            IList<Customer> list = new List<Customer>();

            // Loop over datatable rows first to preserve sort order
            foreach (DataRow row in dt.Rows)
            {
                foreach (Customer customer in customers)
                {
                    if (int.Parse(row["CustomerId"].ToString()) == customer.CustomerId)
                    {
                        customer.NumOrders = int.Parse(row["NumOrders"].ToString());
                        customer.LastOrderDate = DateTime.Parse(row["LastOrderDate"].ToString());

                        list.Add(customer);
                        break;
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Gets all orders for a customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>List of orders.</returns>
        public IList<Order> GetOrders(Customer customer)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT OrderId, OrderDate, RequiredDate, Freight ");
            sql.Append("   FROM [Order]");
            sql.Append("  WHERE CustomerId = " + customer.CustomerId);
            sql.Append(" ORDER BY OrderDate ASC ");

            IList<Order> list = GetOrderList(sql.ToString());
            return list;
        }

        /// <summary>
        /// Gets a list of orders placed within a date range.
        /// </summary>
        /// <param name="dateFrom">Date range begin date.</param>
        /// <param name="dateThru">Date range end date.</param>
        /// <returns>List of orders.</returns>
        public IList<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT OrderId, OrderDate, RequiredDate, Freight ");
            sql.Append("   FROM [Order]");
            sql.Append("  WHERE OrderDate >= '" + dateFrom + "' ");
            sql.Append("    AND OrderDate <= '" + dateThru + "' ");
            sql.Append(" ORDER BY OrderDate ASC ");

            IList<Order> list = GetOrderList(sql.ToString());
            return list;
        }

        /// <summary>
        /// Gets a list of order details for a given order.
        /// </summary>
        /// <param name="order">Unique order identifier.</param>
        /// <returns>List of order details.</returns>
        public IList<OrderDetail> GetOrderDetails(Order order)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ProductName, O.UnitPrice, Quantity, Discount");
            sql.Append("   FROM OrderDetail O INNER JOIN Product ON O.ProductId = Product.ProductId ");
            sql.Append("  WHERE O.OrderId = " + order.OrderId);

            DataTable dt = Db.GetDataTable(sql.ToString());

            IList<OrderDetail> list = new List<OrderDetail>();
            foreach (DataRow row in dt.Rows)
            {
                string product = row["ProductName"].ToString();
                int quantity = int.Parse(row["Quantity"].ToString());
                float unitPrice = float.Parse(row["UnitPrice"].ToString());
                float discount = float.Parse(row["Discount"].ToString());

                list.Add(new OrderDetail(product, quantity, unitPrice, discount, order));
            }
            return list;
        }

        /// <summary>
        /// Gets a list of orders. Private helper method.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>List of orders.</returns>
        private IList<Order> GetOrderList(string sql)
        {
            DataTable dt = Db.GetDataTable(sql);

            IList<Order> list = new List<Order>();
            foreach (DataRow row in dt.Rows)
            {
                int orderId = int.Parse(row["OrderId"].ToString());
                DateTime orderDate = DateTime.Parse(row["OrderDate"].ToString());
                DateTime requiredDate = DateTime.Parse(row["RequiredDate"].ToString());
                float freight = float.Parse(row["Freight"].ToString());

                list.Add(new Order(orderId, orderDate, requiredDate, freight));
            }

            return list;
        }

        /// <summary>
        /// Gets an order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>Order.</returns>
        public Order GetOrder(int orderId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT OrderDate, RequiredDate, Freight ");
            sql.Append("   FROM [Order] ");
            sql.Append("  WHERE OrderId = " + orderId);

            DataRow row = Db.GetDataRow(sql.ToString());

            DateTime orderDate = DateTime.Parse(row["OrderDate"].ToString());
            DateTime requiredDate = DateTime.Parse(row["RequiredDate"].ToString());
            float freight = float.Parse(row["Freight"].ToString());

            return new Order(orderId, orderDate, requiredDate, freight);
        }
    }
}
