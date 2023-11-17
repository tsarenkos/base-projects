using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Base.Application.UseCases.Users.Commands.CreateUser;
using System.Net;

namespace Base.ApiServer.Controllers
{
    [Route("users")]
    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(ILogger<UsersController> logger)
            : base(logger) { }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userId = await this.Mediator.Send(command, cancellationToken);

            return this.Ok(userId);
        }
    }
}
