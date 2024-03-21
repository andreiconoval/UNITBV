using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using DoFactory.BusinessLayer.BusinessObjects;

namespace DoFactory.DataLayer.DataObjects.SqlServer
{
    /// <summary>
    /// Sql Server specific data access object that handles data access
    /// of categories and products.
    /// </summary>
    public class SqlServerProductDao : IProductDao
    {
        /// <summary>
        /// Gets a list of categories.
        /// </summary>
        /// <returns>Category list.</returns>
        public IList<Category> GetCategories()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT CategoryId, CategoryName, Description ");
            sql.Append("   FROM Category");

            DataTable dt = Db.GetDataTable(sql.ToString());

            IList<Category> list = new List<Category>();
            foreach (DataRow row in dt.Rows)
            {
                int id = int.Parse(row["CategoryId"].ToString());
                string name = row["CategoryName"].ToString();
                string description = row["Description"].ToString();

                list.Add(new Category(id, name, description));
            }
            return list;
        }

        /// <summary>
        /// Gets a list of products for a given category.
        /// </summary>
        /// <param name="categoryId">Unique category identifier.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of products.</returns>
        public IList<Product> GetProductsByCategory(int categoryId, string sortExpression)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock ");
            sql.Append("   FROM Product INNER JOIN Category ON Product.CategoryId = Category.CategoryId ");
            sql.Append("  WHERE Category.CategoryId = " + categoryId);
            if (!String.IsNullOrEmpty(sortExpression))
                sql.Append(" ORDER BY " + sortExpression);

            IList<Product> list = GetProductList(sql.ToString());
            return list;
        }

        /// <summary>
        /// Performs a search for products given several criteria.
        /// </summary>
        /// <param name="productName">Product name criterium.</param>
        /// <param name="priceFrom">Low end of price range.</param>
        /// <param name="priceThru">High end of price range.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of products.</returns>
        public IList<Product> SearchProducts(string productName, double? priceFrom, double? priceThru, string sortExpression)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock ");
            sql.Append("   FROM Product ");

            bool where = false;
            if (!String.IsNullOrEmpty(productName))
            {
                sql.Append("  WHERE ProductName LIKE '" + productName + "%' ");
                where = true;
            }

            if (priceFrom != null)
            {
                if (where)
                    sql.Append("   AND UnitPrice >= " + priceFrom);
                else
                    sql.Append(" WHERE UnitPrice >= " + priceFrom);

                sql.Append(" AND UnitPrice <= " + priceThru);
            }

            if (!String.IsNullOrEmpty(sortExpression))
                sql.Append(" ORDER BY " + sortExpression);

            IList<Product> list = GetProductList(sql.ToString());
            return list;
        }

        /// <summary>
        /// Gets a product list. A private helper method.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>List of products.</returns>
        private IList<Product> GetProductList(string sql)
        {
            DataTable dt = Db.GetDataTable(sql.ToString());

            IList<Product> list = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                int productId = int.Parse(row["ProductId"].ToString());
                string name = row["ProductName"].ToString();
                string weight = row["Weight"].ToString();
                double unitPrice = double.Parse(row["UnitPrice"].ToString());
                int unitsInStock = int.Parse(row["UnitsInStock"].ToString());

                list.Add(new Product(productId, name, weight, unitPrice, unitsInStock));
            }
            return list;
        }

        /// <summary>
        /// Gets a product.
        /// </summary>
        /// <param name="id">Unique product identifier.</param>
        /// <returns>Product.</returns>
        public Product GetProduct(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock, ");
            sql.Append("        Category.CategoryId, CategoryName, Description ");
            sql.Append("   FROM Product INNER JOIN Category ON Product.CategoryId = Category.CategoryId ");
            sql.Append("  WHERE Product.ProductId = " + id);

            DataRow row = Db.GetDataRow(sql.ToString());

            int productId = int.Parse(row["ProductId"].ToString());
            string productName = row["ProductName"].ToString();
            string weight = row["weight"].ToString();
            double unitPrice = double.Parse(row["UnitPrice"].ToString());
            int unitsInStock = int.Parse(row["UnitsInStock"].ToString());

            Product product = new Product(productId, productName, weight, unitPrice, unitsInStock);

            return product;
        }

        /// <summary>
        /// Gets a category for a given product.
        /// </summary>
        /// <param name="productId">Unique product identifier.</param>
        /// <returns>Category.</returns>
        public Category GetCategoryByProduct(int productId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Category.CategoryId, CategoryName, Description ");
            sql.Append("  FROM Category INNER JOIN Product ON Product.CategoryId = Category.CategoryId ");
            sql.Append(" WHERE Product.ProductId = " + productId);

            DataRow row = Db.GetDataRow(sql.ToString());
            int categoryId = int.Parse(row["CategoryId"].ToString());
            string categoryName = row["CategoryName"].ToString();
            string description = row["Description"].ToString();

            Category category = new Category(categoryId, categoryName, description);
            return category;
        }
    }
}
