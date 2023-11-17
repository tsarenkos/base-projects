using Base.Enums;
using MediatR;

namespace Base.Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommand: IRequest<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public IList<Guid> ProductCategoryIds { get; set; } = new List<Guid>();
    }
}
