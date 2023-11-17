using Base.Application.Common.Interfaces;
using Base.Application.UseCases.ProductCategories.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.UseCases.ProductCategories.Queries.GetProductCategoriesList
{
    public class GetProductCategoriesListQueryHandler : IRequestHandler<GetProductCategoriesListQuery, IList<ProductCategoryListItem>>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetProductCategoriesListQueryHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IList<ProductCategoryListItem>> Handle(GetProductCategoriesListQuery command, CancellationToken cancellationToken)
        {
            var productCategories = await _dbContext.ProductCategories                
                .OrderBy(x => x.Name)
                .Select(x => x.CreateProductCategoryListItemFromEntity())
                .ToListAsync(cancellationToken);

            return productCategories;
        }
    }
}
