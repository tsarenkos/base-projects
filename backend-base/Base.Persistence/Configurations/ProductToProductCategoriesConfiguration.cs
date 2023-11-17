using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.Domain.Entities;

namespace Base.Persistence.Configurations
{
    public class ProductToProductCategoriesConfiguration
        : IEntityTypeConfiguration<ProductToProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductToProductCategory> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.ProductCategoryId });

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductToProductCategories)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.ProductToProductCategories)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
