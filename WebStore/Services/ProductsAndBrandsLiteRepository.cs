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

			if (filter?.ProductsId is not null)
				if (filter.ProductsId.Any())
					return products.Where(p => filter.ProductsId.Contains(p.Id));

			if (filter?.BrandId is not null)
				products = products.Where(p => p.BrandId == filter.BrandId);

			if (filter?.SectionId is not null)
				products = products.Where(p => p.SectionId == filter.SectionId);

			return products;
		}

		public IEnumerable<Brand> GetBrands() => TestDataService.Brands;

		public IEnumerable<ProductSection> GetProductSections() => TestDataService.ProductSections;

		public void AddProduct(Product source)
		{
			throw new System.NotImplementedException("Only data-reading class.");
		}

		public Product GetProductById(int id) => TestDataService.Products.FirstOrDefault(p => p.Id == id);

        public int GetProductCount() => TestDataService.Products.Count();
    }
}
