using Ninject.Modules;
using RepositoryPattern.BL;
using RepositoryPattern.DAL;
using RepositoryPattern.Interfaces.Bussiness;
using RepositoryPattern.Interfaces.DataAccess;

namespace RepositoryPattern.Startup
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            // Here I should have a switch to change impl (Mocks / Real)

            LoadRepositoryLayer();
            LoadServicesLayer();
        }

        private void LoadServicesLayer()
        {
            Bind<ICategoryService>().To<CategoryService>();
        }

        private void LoadRepositoryLayer()
        {
            Bind<ICategoryRepository>().To<CategoryRepository>();
        }
    }
}
