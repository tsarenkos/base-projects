using Base.Application.Common.Interfaces;
using Base.Application.UseCases.Products.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.UseCases.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, IList<ProductListItem>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetProductsListQueryHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IList<ProductListItem>> Handle(GetProductsListQuery command, CancellationToken cancellationToken)
        {
            var products = await this._dbContext.Products
                .Include(x => x.ProductToProductCategories)
                    .ThenInclude(x => x.ProductCategory)
                .OrderBy(x => x.Name)
                .Select(x => x.CreateProductListItemFromEntity())                
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
