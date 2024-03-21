using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DoFactory.Framework.Cart
{
    /// <summary>
    /// A non-persistent shopping cart.
    /// 
    /// GoF Design Patterns: Strategy.
    /// Enterprise Design Patterns: Table Module, Data Table Gateway.
    /// </summary>
    /// <remarks>
    /// The shopping cart data is stored in a DataTable. It would be easy to 
    /// change this to a persistent cart using a database table that mirrors
    /// this DataTable. You would need to a few methods to the CustomerFacade.
    /// 
    /// The Strategy Pattern is the 'pluggability' of the different shipping methods. 
    /// 
    /// The Table Module Pattern states that it handles domain logic with one class 
    /// per table.  This is essentially what the Cart's DataTable is.
    /// 
    /// The Data Table Gateway Pattern would be implemented if the shopping cart were
    /// to be made persistent (as explained above). One instance would handle all rows
    /// in the database table.
    /// </remarks>
    public class ShoppingCart
    {
        // The shopping cart DataTable.
        private DataTable _dt;

        private double _subTotal;
        private double _total;
        private double _shipping;

        // Pluggable shipping strategy
        private IShipping _shippingStrategy;

        // When cart is 'dirty' recalculations are required
        private bool _isDirty = false;  

        /// <summary>
        /// Default constructor for shopping cart. 
        /// The internal DataTable is configured here.
        /// Fedex is the default shipping strategy.
        /// </summary>
        public ShoppingCart()
        {
            _dt = new DataTable("Cart");
            _dt.Columns.Add("Id", typeof(int));
            _dt.Columns.Add("Name", typeof(string));
            _dt.Columns.Add("Quantity", typeof(int));
            _dt.Columns.Add("UnitPrice", typeof(double));

            // Fedex is the default (in case web.config setting is not used).
            _shippingStrategy = new ShippingStrategyFedex();
        }

        /// <summary>
        /// Adds a product item to the shopping cart.
        /// </summary>
        /// <param name="id">Unique product identifier.</param>
        /// <param name="name">Product name.</param>
        /// <param name="quantity">Quantity.</param>
        /// <param name="unitPrice">Unit price of product.</param>
        public void AddItem(int id, string name, int quantity, double unitPrice)
        {
            _isDirty = true;

            // Iterate over all items to check whether it already exist?
            foreach (DataRow row in _dt.Rows)
            {
                
                if (int.Parse(row["Id"].ToString()) == id)
                {
                    // Same item, just cumulate quantity
                    row["Quantity"] = int.Parse(row["Quantity"].ToString()) + quantity;
                    return;
                }
            }

            DataRow newRow = _dt.NewRow();

            newRow["Id"] = id;
            newRow["Name"] = name;
            newRow["Quantity"] = quantity;
            newRow["UnitPrice"] = unitPrice;

            _dt.Rows.Add(newRow);
        }

        /// <summary>
        /// Removes a product item from the shopping cart.
        /// </summary>
        /// <param name="id">Unique product identifier.</param>
        public void RemoveItem(int id)
        {
            _isDirty = true;

            foreach (DataRow row in _dt.Rows)
            {
                if (int.Parse(row["Id"].ToString()) == id)
                {
                    _dt.Rows.Remove(row);
                    return;
                }
            }
        }

        /// <summary>
        /// Updates quantity for a given product in shopping cart.
        /// </summary>
        /// <param name="id">Unique product identifier.</param>
        /// <param name="quantity">New quantity.</param>
        public void UpdateQuantity(int id, int quantity)
        {
            _isDirty = true;

            foreach (DataRow row in _dt.Rows)
            {
                if (int.Parse(row["Id"].ToString()) == id)
                {
                    row["Quantity"] = quantity;
                    return;
                }
            }
        }

        /// <summary>
        /// Recalculates the total, subtotals, and shipping costs.
        /// </summary>
        private void ReCalculate()
        {
            // No need to calculate if nothing was changed
            if (!_isDirty) return;

            _subTotal = 0.0;
            _shipping = 0.0;

            foreach (DataRow row in _dt.Rows)
            {
                int quantity = int.Parse(row["Quantity"].ToString());
                double unitPrice = double.Parse(row["UnitPrice"].ToString());

                _subTotal += unitPrice * quantity;
                _shipping += _shippingStrategy.EstimateShipping(unitPrice, quantity);
            }

            // Add subtotal and shipping to get total
            _total = _subTotal + _shipping;

            _isDirty = false;
        }

        /// <summary>
        /// Gets subtotal for all items in the shopping cart. Recalculate if needed.
        /// </summary>
        public double SubTotal
        {
            get
            {
                ReCalculate();
                return _subTotal;
            }
        }

        /// <summary>
        /// Gets total for all items in shopping cart. Recalculate if needed.
        /// </summary>
        public double Total
        {
            get
            {
                ReCalculate();
                return _total;
            }
        }

        /// <summary>
        /// Gets shipping cost for all items in shopping cart. Recalculate if needed.
        /// </summary>
        public double Shipping
        {
            get
            {
                ReCalculate();
                return _shipping;
            }
        }

        /// <summary>
        /// Gets number of items in shopping cart.
        /// </summary>
        public int ItemCount
        {
            get { return _dt.Rows.Count; }
        }

        /// <summary>
        /// Gets datatable holding shopping cart data.
        /// </summary>
        public DataTable DataTable
        {
            get { return _dt; }
        }

        /// <summary>
        /// Gets or sets shipping 'strategy', i.e. the means at which products are shipped.
        /// This is the Strategy Design Pattern in action.
        /// </summary>
        public IShipping ShippingStrategy
        {
            set
            {
                _shippingStrategy = value;
                _isDirty = true;
            }
            get { return _shippingStrategy; }
        }
    }
}
