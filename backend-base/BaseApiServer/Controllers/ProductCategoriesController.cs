using Base.Application.UseCases.ProductCategories.Commands.CreateProductCategory;
using Base.Application.UseCases.ProductCategories.Commands.DeleteProductCategory;
using Base.Application.UseCases.ProductCategories.Commands.UpdateProductCategory;
using Base.Application.UseCases.ProductCategories.Queries.GetProductCategoriesList;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Base.Application.UseCases.ProductCategories.Models;
using Base.Application.UseCases.ProductCategories.Queries.GetProductCategory;
using Microsoft.AspNetCore.Authorization;

namespace Base.ApiServer.Controllers
{
    [Route("productCategories")]
    [Authorize]
    public class ProductCategoriesController : BaseController
    {
        public ProductCategoriesController(ILogger<ProductCategoriesController> logger)
            : base(logger) { }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductCategoryListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetProductCategoriesListQuery();
            var categories = await this.Mediator.Send(query, cancellationToken);

            return this.Ok(categories);
        }

        [HttpGet("{categoryId:guid}")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductCategorySummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromRoute]Guid categoryId, CancellationToken cancellationToken)
        {
            var query = new GetProductCategoryQuery { Id = categoryId };
            var category = await this.Mediator.Send(query, cancellationToken);

            return this.Ok(category);
        }
     
        [HttpPost]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CreateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var categoryId = await this.Mediator.Send(command, cancellationToken);

            return this.Ok(categoryId);
        }

        [HttpPut("{categoryId:guid}")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Put([FromRoute] Guid categoryId, [FromBody] UpdateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            command.Id = categoryId;
            await this.Mediator.Send(command, cancellationToken);

            return this.NoContent();
        }

        [HttpDelete("{categoryId:guid}")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]        
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCategoryCommand { Id = categoryId };
            await this.Mediator.Send(command, cancellationToken);

            return this.NoContent();
        }
    }
}
