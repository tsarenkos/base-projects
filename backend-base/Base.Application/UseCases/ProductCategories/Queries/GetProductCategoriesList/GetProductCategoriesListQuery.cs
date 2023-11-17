using Base.Application.UseCases.ProductCategories.Models;
using MediatR;

namespace Base.Application.UseCases.ProductCategories.Queries.GetProductCategoriesList
{
    public class GetProductCategoriesListQuery: IRequest<IList<ProductCategoryListItem>>
    {
    }
}
