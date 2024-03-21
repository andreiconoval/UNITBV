using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using DoFactory.BusinessLayer.BusinessObjects;

namespace DoFactory.DataLayer.DataObjects.Access
{
    /// <summary>
    /// Microsoft Access specific data access object that handles data access
    /// of customers.
    /// </summary>
    public class AccessCustomerDao : ICustomerDao
    {
        /// <summary>
        /// Gets a list of all customers.
        /// </summary>
        /// <returns>Customer list.</returns>
        public IList<Customer> GetCustomers()
        {
            string sortExpression = null;
            return GetCustomers(sortExpression);
        }

        /// <summary>
        /// Gets a sorted list of all customers.
        /// </summary>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of customers.</returns>
        public IList<Customer> GetCustomers(string sortExpression)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CustomerId, CompanyName, City, Country ");
            sql.Append("   FROM Customer ");
            if (!String.IsNullOrEmpty(sortExpression))
                sql.Append(" ORDER BY " + sortExpression);

            DataTable dt = Db.GetDataTable(sql.ToString());

            IList<Customer> list = new List<Customer>();
            foreach (DataRow row in dt.Rows)
            {
                int id = int.Parse(row["CustomerId"].ToString());
                string company = row["CompanyName"].ToString();
                string city = row["City"].ToString();
                string country = row["Country"].ToString();

                list.Add(new Customer(id, company, city, country));
            }
            return list;
        }

        /// <summary>
        /// Gets a customer.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Customer.</returns>
        public Customer GetCustomer(int customerId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CompanyName, City, Country ");
            sql.Append("   FROM Customer");
            sql.Append("  WHERE CustomerId = " + customerId);

            DataRow row = Db.GetDataRow(sql.ToString());

            string company = row["CompanyName"].ToString();
            string city = row["City"].ToString();
            string country = row["Country"].ToString();

            return new Customer(customerId, company, city, country);
        }

        /// <summary>
        /// Gets customer given an order.
        /// </summary>
        /// <param name="order">Order.</param>
        /// <returns>Customer.</returns>
        public Customer GetCustomerByOrder(Order order)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT Customer.CustomerId, CompanyName, City, Country ");
            sql.Append("   FROM [Order] LEFT JOIN Customer ON [Order].CustomerId = Customer.CustomerId ");
            sql.Append("  WHERE OrderId = " + order.OrderId);

            DataRow row = Db.GetDataRow(sql.ToString());

            int customerId = int.Parse(row["CustomerId"].ToString());
            string company = row["CompanyName"].ToString();
            string city = row["City"].ToString();
            string country = row["Country"].ToString();

            Customer customer = new Customer(customerId, company, city, country);
            order.Customer = customer;

            return customer;
        }

        /// <summary>
        /// Inserts a new customer. 
        /// </summary>
        /// <remarks>
        /// Following insert, customer object will contain the new identifier.
        /// </remarks>
        /// <param name="customer">Customer.</param>
        public void InsertCustomer(Customer customer)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" INSERT INTO Customer (CompanyName, City, Country) ");
            sql.Append("  VALUES( " + Db.Escape(customer.Company) + ", ");
            sql.Append("          " + Db.Escape(customer.City)    + ", ");
            sql.Append("          " + Db.Escape(customer.Country) + ") ");

            // Assign new customer Id back to business object
            int id = Db.Insert(sql.ToString(),true);
            customer.CustomerId = id;
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Number of customer records updated.</returns>
        public int UpdateCustomer(Customer customer)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" UPDATE Customer ");
            sql.Append("  SET CompanyName = " + Db.Escape(customer.Company) + ", ");
            sql.Append("      City = " + Db.Escape(customer.City) + ", ");
            sql.Append("      Country = " + Db.Escape(customer.Country) + " ");
            sql.Append("  WHERE CustomerId = " + customer.CustomerId);

            return Db.Update(sql.ToString());
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Number of customer records deleted.</returns>
        public int DeleteCustomer(Customer customer)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" DELETE FROM Customer  ");
            sql.Append("  WHERE CustomerId = " + customer.CustomerId );

            return Db.Update(sql.ToString());
        }
    }
}
