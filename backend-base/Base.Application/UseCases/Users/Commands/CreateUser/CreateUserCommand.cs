using MediatR;

namespace Base.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
