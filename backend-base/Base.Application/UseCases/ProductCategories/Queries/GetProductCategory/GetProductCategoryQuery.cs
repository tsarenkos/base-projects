using Base.Application.UseCases.ProductCategories.Models;
using MediatR;

namespace Base.Application.UseCases.ProductCategories.Queries.GetProductCategory
{
    public class GetProductCategoryQuery : IRequest<ProductCategorySummary>
    {
        public Guid Id { get; set; }
    }
}
