using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Persistence.Configurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Currency)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.LastModifiedAt)
                .IsRequired(false)
                .HasColumnType("datetime2");
        }
    }
}
