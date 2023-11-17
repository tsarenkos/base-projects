using Base.Application.UseCases.ProductCategories.Models;
using Base.Enums;

namespace Base.Application.UseCases.Products.Models
{
    public class ProductSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public IList<ProductCategorySummary> ProductCategories { get; set; } = new List<ProductCategorySummary>();
    }
}
