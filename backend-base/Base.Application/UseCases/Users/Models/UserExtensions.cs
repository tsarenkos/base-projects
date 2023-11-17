using Base.Common;
using Base.Domain.Entities;

namespace Base.Application.UseCases.Users.Commands.CreateUser.Models
{
    public static class UserExtensions
    {
        public static User CreateUserFromCommand(this CreateUserCommand command)
        {
            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                UserRole = Enums.UserRole.User,
                Password = CryptoHelper.HashPassword(command.Password)                
            };            

            return user;
        }        
    }
}
