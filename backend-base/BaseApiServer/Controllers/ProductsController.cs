using Base.Application.UseCases.Products.Commands.CreateProduct;
using Base.Application.UseCases.Products.Commands.DeleteProduct;
using Base.Application.UseCases.Products.Commands.UpdateProduct;
using Base.Application.UseCases.Products.Models;
using Base.Application.UseCases.Products.Queries.GetProduct;
using Base.Application.UseCases.Products.Queries.GetProductsList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Base.ApiServer.Controllers
{
    [Route("products")]
    [Authorize]
    public class ProductsController : BaseController
    {
        public ProductsController(ILogger<ProductsController> logger)
            : base(logger) { }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetProductsListQuery();
            var products = await this.Mediator.Send(query, cancellationToken);

            return this.Ok(products);
        }

        [HttpGet("{productId:guid}")]
        [ProducesResponseType(typeof(ProductSummary), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute]Guid productId, CancellationToken cancellationToken)
        {
            var query = new GetProductQuery { Id = productId };
            var product = await this.Mediator.Send(query, cancellationToken);

            return this.Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CreateProductCommand command, CancellationToken cancellationToken)
        {
            var productId = await this.Mediator.Send(command, cancellationToken);

            return this.Ok(productId);
        }

        [HttpPut("{productId:guid}")]        
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Put([FromRoute] Guid productId, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            command.Id = productId;
            await this.Mediator.Send(command, cancellationToken);

            return this.NoContent();
        }

        [HttpDelete("{productId:guid}")]        
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid productId, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand { Id = productId };
            await this.Mediator.Send(command, cancellationToken);

            return this.NoContent();
        }
    }
}
