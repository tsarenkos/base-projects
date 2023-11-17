using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Base.ApiServer.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private ISender _mediator = null!;
        protected ISender Mediator =>
            this._mediator ??= this.HttpContext.RequestServices.GetRequiredService<ISender>();
        protected ILogger Logger { get; }

        public BaseController(ILogger logger)
            : base() => this.Logger = logger;
    }
}