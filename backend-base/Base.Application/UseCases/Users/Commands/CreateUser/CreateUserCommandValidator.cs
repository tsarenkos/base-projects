using FluentValidation;

namespace Base.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator
        : AbstractValidator<CreateUserCommand>
    {
        //TODO: Add validation rules later
        public CreateUserCommandValidator()
        {            
            this.RuleFor(x => x.FirstName)
                .NotEmpty();

            this.RuleFor(x =>  x.LastName)
                .NotEmpty();

            this.RuleFor(x => x.Email)
                .NotEmpty();

            this.RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
