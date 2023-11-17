using Base.Domain.Common;
using Base.Enums;

namespace Base.Domain.Entities
{
    public class Product: IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public IList<ProductToProductCategory> ProductToProductCategories { get; set; } = new List<ProductToProductCategory>();
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
