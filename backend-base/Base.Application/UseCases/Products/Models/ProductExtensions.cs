using Base.Application.UseCases.ProductCategories.Models;
using Base.Application.UseCases.Products.Commands.CreateProduct;
using Base.Application.UseCases.Products.Commands.UpdateProduct;
using Base.Domain.Entities;

namespace Base.Application.UseCases.Products.Models
{
    public static class ProductExtensions
    {
        public static Product CreateProductFromCommand(this CreateProductCommand command)
        {           
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price,
                Currency = command.Currency
            };

            if (command.ProductCategoryIds.Any())
            {
                product.ProductToProductCategories = command.ProductCategoryIds
                    .Select(id => new ProductToProductCategory
                    {
                        ProductCategoryId = id,
                    }).ToList();
            }

            return product;
        }

        public static void UpdateProductFromCommand(this UpdateProductCommand command, Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            product.Name = command.Name;
            product.Price = command.Price;
            product.Currency = command.Currency;

            if (command.ProductCategoryIds.Any())
            {
                product.ProductToProductCategories = command.ProductCategoryIds
                    .Select(id => new ProductToProductCategory
                    {
                        ProductCategoryId = id,
                        ProductId = product.Id
                    }).ToList();
            }
        }

        public static ProductListItem CreateProductListItemFromEntity(this Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var listItem = new ProductListItem
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Currency = product.Currency,                
            };

            if (product.ProductToProductCategories.Any())
            {
                listItem.ProductCategories = product.ProductToProductCategories
                    .Select(x => new ProductCategorySummary
                    {
                        Id = x.ProductCategoryId,
                        Name = x.ProductCategory.Name
                    }).ToList();
            }

            return listItem;
        }

        public static ProductSummary CreateProductSummaryFromEntity(this Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var productSummary = new ProductSummary
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Currency = product.Currency,
            };

            if (product.ProductToProductCategories.Any())
            {
                productSummary.ProductCategories = product.ProductToProductCategories
                    .Select(x => new ProductCategorySummary
                    {
                        Id = x.ProductCategoryId,
                        Name = x.ProductCategory.Name
                    }).ToList();
            }

            return productSummary;
        }
    }
}
