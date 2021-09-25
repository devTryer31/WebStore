using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
	internal class ProductsAndBrandsLiteRepository : IProductsAndBrandsLiteRepository
	{
		public IEnumerable<Product> GetProducts() => TestDataService.Products;

		public IEnumerable<Brand> GetBrands() => TestDataService.Brands;

		public IEnumerable<ProductSection> GetProductSections() => TestDataService.ProductSections;
	}
}
