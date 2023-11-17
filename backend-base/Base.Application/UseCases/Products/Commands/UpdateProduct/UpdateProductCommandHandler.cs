using Base.Application.Common.Exceptions;
using Base.Application.Common.Interfaces;
using Base.Application.UseCases.Products.Models;
using Base.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateProductCommandHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await this._dbContext.Products
                .Include(x => x.ProductToProductCategories)
                .Where(x => x.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (product is null) 
            {
                throw new NotFoundException(nameof(Product), command.Id);
            }

            command.UpdateProductFromCommand(product);

            await this._dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
