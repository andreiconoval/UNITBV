using RepositoryPattern.Interfaces.DataAccess;
using RepositoryPattern.Models;

namespace RepositoryPattern.DAL
{
    class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public override void Delete(Category entityToDelete)
        {
            //base.Delete(entityToDelete);
        }
    }
}
