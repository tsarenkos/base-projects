using Base.Application.Common.Interfaces;
using Base.Application.UseCases.Products.Models;
using MediatR;

namespace Base.Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateProductCommandHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = command.CreateProductFromCommand();

            await this._dbContext.Products.AddAsync(product, cancellationToken);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
