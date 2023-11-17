using Base.Application.Common.Interfaces;
using Base.Domain.Entities;
using Base.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Base.Persistence
{
    public class ApplicationDbContext
        : DbContext, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _interceptor;
        public DbSet<User> Users => this.Set<User>();
        public DbSet<Product> Products => this.Set<Product>();
        public DbSet<ProductCategory> ProductCategories => this.Set<ProductCategory>();
        public DbSet<ProductToProductCategory> ProductToProductCategories => this.Set<ProductToProductCategory>();

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options, 
            AuditableEntitySaveChangesInterceptor interceptor
            ) : base(options) 
        {
            this._interceptor = interceptor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(this._interceptor);
        }      
    }
}
