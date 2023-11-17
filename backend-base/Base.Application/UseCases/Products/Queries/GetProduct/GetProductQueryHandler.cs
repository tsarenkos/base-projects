using Base.Application.Common.Exceptions;
using Base.Application.Common.Interfaces;
using Base.Application.UseCases.Products.Models;
using Base.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.UseCases.Products.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductSummary>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetProductQueryHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<ProductSummary> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await this._dbContext.Products
                .Include(x => x.ProductToProductCategories)
                    .ThenInclude(x => x.ProductCategory)
                .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);                           

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), query.Id);
            }

            var productSummary = product.CreateProductSummaryFromEntity();

            return productSummary;
        }
    }
}
