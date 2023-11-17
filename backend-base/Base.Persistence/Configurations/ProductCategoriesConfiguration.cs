using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Persistence.Configurations
{
    public class ProductCategoriesConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();                

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.CreatedAt)
                .IsRequired()              
                .HasColumnType("datetime2");

            builder.Property(x => x.LastModifiedAt)
                .IsRequired(false)            
                .HasColumnType("datetime2");
        }
    }
}
