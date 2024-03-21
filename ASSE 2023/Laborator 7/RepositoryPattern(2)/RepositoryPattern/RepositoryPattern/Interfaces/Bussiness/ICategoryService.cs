using RepositoryPattern.Models;
using System.Collections.Generic;

namespace RepositoryPattern.Interfaces.Bussiness
{
    interface ICategoryService : IService<Category>
    {
        IEnumerable<Category> GetCategoriesWithProducts();
    }
}
