using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DemoEF6CodeFirst.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDomainModel
{
    [TestClass]
    public class ProductTests
    {
        private Category category;
        private Product product;

        [TestInitialize]
        public void SetUp() 
        {
            this.category = new Category
            {
                Name = "Produse alimentare pentru oameni",
                Description = "produse de larg consum",
            };

            this.product = new Product
            {
                Name = "a product",
                UnitPrice = 3.5m,
                MeasurementUnit = "piece",
                Category = this.category
            };
        }

        [TestMethod]
        public void TestCorrectProduct()
        {
            var context = new ValidationContext(this.product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(product, context, results));
        }


        [TestMethod]
        public void TestNullName()
        {
            product.Name = null;

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results));
        }

        [TestMethod]
        public void TestMissingCategory()
        {
            product.Category = null;

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results));
        }
    }
}
