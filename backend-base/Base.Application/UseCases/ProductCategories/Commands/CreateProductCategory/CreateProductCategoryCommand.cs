using MediatR;

namespace Base.Application.UseCases.ProductCategories.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand: IRequest<Guid>
    {
        public string Name { get; set; }       
    }
}
