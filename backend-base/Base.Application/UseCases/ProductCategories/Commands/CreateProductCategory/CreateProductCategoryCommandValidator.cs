using FluentValidation;

namespace Base.Application.UseCases.ProductCategories.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandValidator 
        : AbstractValidator<CreateProductCategoryCommand>
    {
        public CreateProductCategoryCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
