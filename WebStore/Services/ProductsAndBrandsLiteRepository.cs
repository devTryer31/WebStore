using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
	public class ProductsAndBrandsLiteRepository : IProductsAndBrandsLiteRepository
	{
		public IEnumerable<Product> GetProducts(ProductsFilter filter = null)
		{
			var products = TestDataService.Products;
			
			if (filter?.BrandId is not null)
				products = products.Where(p => p.BrandId == filter.BrandId);

			if (filter?.SectionId is not null)
				products = products.Where(p => p.SectionId == filter.SectionId);

			return products;
		}

		public IEnumerable<Brand> GetBrands() => TestDataService.Brands;

		public IEnumerable<ProductSection> GetProductSections() => TestDataService.ProductSections;
	}
}
