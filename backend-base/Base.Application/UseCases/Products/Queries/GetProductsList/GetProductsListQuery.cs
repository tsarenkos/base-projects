using Base.Application.UseCases.Products.Models;
using MediatR;

namespace Base.Application.UseCases.Products.Queries.GetProductsList
{
    public class GetProductsListQuery : IRequest<IList<ProductListItem>>
    {
    }
}
