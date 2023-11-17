using Base.Application.Common.Exceptions;
using Base.Application.Common.Interfaces;
using Base.Application.UseCases.ProductCategories.Models;
using Base.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.UseCases.ProductCategories.Queries.GetProductCategory
{
    public class GetProductCategoryQueryHandler : IRequestHandler<GetProductCategoryQuery, ProductCategorySummary>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetProductCategoryQueryHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<ProductCategorySummary> Handle(GetProductCategoryQuery query, CancellationToken cancellationToken)
        {
            var category = await _dbContext.ProductCategories.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (category is null)
            {
                throw new NotFoundException(nameof(ProductCategory), query.Id);
            }

            var categorySummary = category.CreateProductCategorySummaryFromEntity();

            return categorySummary;
        }
    }
}
