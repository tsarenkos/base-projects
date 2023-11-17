using Base.Enums;
using FluentValidation;

namespace Base.Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator 
        : AbstractValidator<CreateProductCommand>
    {
        //TODO: Add validation rules later
        public CreateProductCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty();

            this.RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);

            this.RuleFor(x => x.Currency)
                .NotEmpty()
                .Must(x => Enum.IsDefined(typeof(Currency), x));
        }
    }
}
