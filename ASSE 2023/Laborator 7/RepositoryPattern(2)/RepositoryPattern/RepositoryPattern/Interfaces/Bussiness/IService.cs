using FluentValidation.Results;
using System.Collections.Generic;

namespace RepositoryPattern.Interfaces.Bussiness
{
    interface IService<T>
        where T : class
    {
        ValidationResult Insert(T entity);

        ValidationResult Update(T entity);

        void Delete(T entity);

        T GetByID(object id);

        IEnumerable<T> GetAll();
    }
}
