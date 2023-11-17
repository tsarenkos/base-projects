using Base.Domain.Common;

namespace Base.Domain.Entities
{
    public class ProductCategory : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<ProductToProductCategory> ProductToProductCategories { get; set; } = new List<ProductToProductCategory>();
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
