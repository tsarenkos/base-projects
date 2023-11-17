using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Base.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Product> Products { get; }
        public DbSet<ProductCategory> ProductCategories { get; }
        public DbSet<ProductToProductCategory> ProductToProductCategories { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
