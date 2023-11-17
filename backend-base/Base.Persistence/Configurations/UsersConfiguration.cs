using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Persistence.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();                

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.UserRole)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.IsEmailConfirmed)
                .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired()               
                .HasColumnType("datetime2");

            builder.Property(x => x.LastModifiedAt)
                .IsRequired(false)              
                .HasColumnType("datetime2");
        }
    }
}
