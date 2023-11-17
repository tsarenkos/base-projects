namespace Base.Domain.Entities
{
    public class ProductToProductCategory
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
