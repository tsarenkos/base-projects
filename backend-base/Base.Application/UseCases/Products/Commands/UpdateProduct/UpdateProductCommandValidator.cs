using Base.Enums;
using FluentValidation;

namespace Base.Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator
        : AbstractValidator<UpdateProductCommand>
    {
        //TODO: Add validation rules
        public UpdateProductCommandValidator()
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
