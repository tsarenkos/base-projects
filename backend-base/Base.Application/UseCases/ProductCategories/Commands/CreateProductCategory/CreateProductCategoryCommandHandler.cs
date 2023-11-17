using Base.Application.Common.Interfaces;
using Base.Application.UseCases.ProductCategories.Models;
using MediatR;

namespace Base.Application.UseCases.ProductCategories.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateProductCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = command.CreateProductCategoryFromCommand();

            await this._dbContext.ProductCategories.AddAsync(category, cancellationToken);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
