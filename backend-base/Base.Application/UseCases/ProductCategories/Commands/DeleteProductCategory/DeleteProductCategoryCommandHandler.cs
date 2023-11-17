using Base.Application.Common.Exceptions;
using Base.Application.Common.Interfaces;
using Base.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.UseCases.ProductCategories.Commands.DeleteProductCategory
{
    public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteProductCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await this._dbContext.ProductCategories.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (category == null) 
            {
                throw new NotFoundException(nameof(ProductCategory), command.Id);
            }

            this._dbContext.ProductCategories.Remove(category);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
