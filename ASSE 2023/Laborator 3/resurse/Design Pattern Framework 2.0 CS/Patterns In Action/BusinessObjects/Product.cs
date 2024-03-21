using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds product information.
    /// 
    /// Enterprise Design Pattern: Domain Model, Identity Field.
    /// </summary>
    /// <remarks>
    /// This is where your business logic resides. In this example there are none.
    /// Another place for business logic and business rules is in the Facade.  
    /// For an example see CustomerFacade in the Facade layer.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// 
    /// The Identity Field Design Pattern saves the ID field in an object to maintain
    /// identity between an in-memory business object and that database rows.
    /// </remarks>
    [Serializable]
    public class Product
    {
        private int _productId;
        private string _productName;
        private string _weight;
        private double _unitPrice;
        private int _unitsInStock;
        private Category _category;  

        /// <summary>
        /// Default constructor for product.
        /// </summary>
        public Product() { }

        /// <summary>
        /// Overloaded constructor for product
        /// </summary>
        /// <param name="productId">Unique identifier for Product</param>
        /// <param name="productName">Name of Product.</param>
        /// <param name="weight">Weight of Product.</param>
        /// <param name="unitPrice">Unit price of Product in US$.</param>
        /// <param name="unitsInStock">Product units in stock.</param>
        public Product(int productId, string productName, string weight, double unitPrice, int unitsInStock)
        {
            this._productId = productId;
            this._productName = productName;
            this._weight = weight;
            this._unitPrice = unitPrice;
            this._unitsInStock = unitsInStock;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// The Identity Field Design Pattern. 
        /// </summary>
        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        /// <summary>
        /// Getd or sets the product name.
        /// </summary>
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        /// <summary>
        /// Gets or sets the weight of the product.
        /// </summary>
        public string Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        /// <summary>
        /// Gets or sets the unit price of the product in US$.
        /// </summary>
        public double UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        /// <summary>
        /// Gets or sets units in stock for the product.
        /// </summary>
        public int UnitsInStock
        {
            get { return _unitsInStock; }
            set { _unitsInStock = value; }
        }

        /// <summary>
        /// Gets or sets the product category under which product is categorized.
        /// </summary>
        public Category Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// Gets category name under which product is categorized.
        /// </summary>
        public string CategoryName
        {
            get { return Category.Name; }
        }
    }
}
