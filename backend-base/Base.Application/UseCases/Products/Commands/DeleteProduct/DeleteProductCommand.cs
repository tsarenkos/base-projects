using MediatR;

namespace Base.Application.UseCases.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand: IRequest<Unit>
    {
        public Guid Id { get; set; }        
    }
}
