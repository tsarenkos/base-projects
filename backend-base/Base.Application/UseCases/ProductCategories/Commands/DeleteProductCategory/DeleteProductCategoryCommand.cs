using MediatR;

namespace Base.Application.UseCases.ProductCategories.Commands.DeleteProductCategory
{
    public class DeleteProductCategoryCommand: IRequest<Unit>
    {
        public Guid Id { get; set; }        
    }
}
