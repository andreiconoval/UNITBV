using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a product category.
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
    public class Category
    {
        private int _categoryId;
        private string _name;
        private string _description;

        /// <summary>
        /// Default constructor for Category class.
        /// </summary>
        public Category() { }

        /// <summary>
        /// Overloaded constructor for Category class.
        /// </summary>
        /// <param name="categoryId">Unique Identifier for the Category.</param>
        /// <param name="name">Name of the Category.</param>
        /// <param name="description">Description of the Category.</param>
        public Category(int categoryId, string name, string description)
        {
            this._categoryId = categoryId;
            this._name = name;
            this._description = description;
        }

        /// <summary>
        /// Gets or sets unique category identifier.
        /// The Identity Field Design Pattern. 
        /// </summary>
        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the category description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
