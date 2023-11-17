using FluentValidation;

namespace Base.Application.UseCases.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommandValidator
        : AbstractValidator<UpdateProductCategoryCommand>
    {
        public UpdateProductCategoryCommandValidator()
        {           
            this.RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}