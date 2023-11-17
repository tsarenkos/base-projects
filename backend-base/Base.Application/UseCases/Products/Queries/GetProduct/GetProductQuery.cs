using Base.Application.UseCases.Products.Models;
using MediatR;

namespace Base.Application.UseCases.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<ProductSummary>
    {
        public Guid Id { get; set; }
    }
}
