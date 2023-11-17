using Base.Application.Common.Exceptions;
using Base.Application.Common.Interfaces;
using Base.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.UseCases.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteProductCommandHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await this._dbContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), command.Id);
            }

            this._dbContext.Products.Remove(product);

            await this._dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
