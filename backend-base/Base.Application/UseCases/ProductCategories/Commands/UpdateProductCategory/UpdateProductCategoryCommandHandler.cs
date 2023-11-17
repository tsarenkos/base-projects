using Base.Application.Common.Exceptions;
using Base.Application.Common.Interfaces;
using Base.Application.UseCases.ProductCategories.Models;
using Base.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.UseCases.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateProductCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await this._dbContext.ProductCategories.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (category is null) 
            {
                throw new NotFoundException(nameof(ProductCategory), command.Id);
            }

            command.UpdateProductCategoryFromCommand(category);

            await this._dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
