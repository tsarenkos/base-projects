using MediatR;
using Newtonsoft.Json;

namespace Base.Application.UseCases.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommand: IRequest<Unit>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }        
    }
}
