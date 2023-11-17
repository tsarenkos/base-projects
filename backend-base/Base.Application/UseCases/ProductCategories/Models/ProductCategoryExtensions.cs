using Base.Application.UseCases.ProductCategories.Commands.CreateProductCategory;
using Base.Application.UseCases.ProductCategories.Commands.UpdateProductCategory;
using Base.Domain.Entities;

namespace Base.Application.UseCases.ProductCategories.Models
{
    public static class ProductCategoryExtensions
    {
        public static ProductCategory CreateProductCategoryFromCommand(this CreateProductCategoryCommand command)
        {
            var category = new ProductCategory
            {
                Name = command.Name
            };

            return category;
        }

        public static void UpdateProductCategoryFromCommand(this UpdateProductCategoryCommand command, ProductCategory category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            category.Name = command.Name;
        }

        public static ProductCategoryListItem CreateProductCategoryListItemFromEntity(this ProductCategory category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var listItem = new ProductCategoryListItem
            {
                Id = category.Id,
                Name = category.Name,
            };

            return listItem;
        }

        public static ProductCategorySummary CreateProductCategorySummaryFromEntity(this ProductCategory category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var categorySummary = new ProductCategorySummary
            {
                Id = category.Id,
                Name = category.Name
            };            

            return categorySummary;
        }
    }
}
