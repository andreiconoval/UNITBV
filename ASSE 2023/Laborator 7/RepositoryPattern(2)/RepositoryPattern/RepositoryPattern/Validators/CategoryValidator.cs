using FluentValidation;
using RepositoryPattern.Models;

namespace RepositoryPattern.Validators
{
    class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name).NotEmpty();
        }
    }
}
