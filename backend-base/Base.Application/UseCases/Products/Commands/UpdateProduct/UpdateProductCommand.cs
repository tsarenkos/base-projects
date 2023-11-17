using Base.Enums;
using MediatR;
using Newtonsoft.Json;

namespace Base.Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand: IRequest<Unit>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public IList<Guid> ProductCategoryIds { get; set; } = new List<Guid>();
    }
}
