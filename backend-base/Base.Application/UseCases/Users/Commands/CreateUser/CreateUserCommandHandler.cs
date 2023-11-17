using Base.Application.Common.Interfaces;
using Base.Application.UseCases.Users.Commands.CreateUser.Models;
using MediatR;

namespace Base.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateUserCommandHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = command.CreateUserFromCommand();

            await this._dbContext.Users.AddAsync(user, cancellationToken);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
