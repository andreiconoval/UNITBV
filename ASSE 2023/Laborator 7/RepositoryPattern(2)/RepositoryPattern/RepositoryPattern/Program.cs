using Ninject;
using RepositoryPattern.Interfaces.Bussiness;
using RepositoryPattern.Models;
using RepositoryPattern.Startup;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern
{
    class Program
    {
        /// <summary>
        /// This will act like a controller.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Injector.Inject();
            var kernel = Injector.Kernel;
            var categoryService = kernel.Get<ICategoryService>();

            //ListAllCategoryNames(categoryService);

            ListAllCategoriesWithProducts(categoryService);

            InsertCategory(categoryService);

            Console.ReadKey(true);
        }

        private static void InsertCategory(ICategoryService categoryService)
        {
            var validationResult = categoryService.Insert(new Category
            {
                Name = "inserted",
                Description = "Descri",
                Products = new List<Product>
                {
                    new Product
                    {
                        MeasurementUnit = "Km",
                        Name = "Produs",
                        UnitPrice = 100
                    }
                }
            });

            Console.WriteLine(validationResult.IsValid);
        }

        private static void ListAllCategoriesWithProducts(ICategoryService categoryService)
        {
            var categories = categoryService.GetCategoriesWithProducts();
            ListCategoryNames(categories);
        }

        static void ListAllCategoryNames(ICategoryService categoryService)
        {
            var allCategories = categoryService.GetAll();
            ListCategoryNames(allCategories);
        }

        static void ListCategoryNames(IEnumerable<Category> categories)
        {
            categories.Select(c => c.Name).ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }
    }
}
