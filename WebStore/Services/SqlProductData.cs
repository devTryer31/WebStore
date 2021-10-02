using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
	public class SqlProductData : IProductsAndBrandsLiteRepository
	{
		private readonly WebStoreDb _Db;

		public SqlProductData(WebStoreDb db) => _Db = db;

		public IEnumerable<Product> GetProducts(ProductsFilter filter = null)
		{
			IQueryable<Product> products = _Db.Products;
			
			if (filter?.BrandId is not null)
				products = products.Where(p => p.BrandId == filter.BrandId);

			if (filter?.SectionId is not null)
				products = products.Where(p => p.SectionId == filter.SectionId);

			return products;
		}

		public IEnumerable<Brand> GetBrands() => _Db.Brands;

		public IEnumerable<ProductSection> GetProductSections() => _Db.ProductSections;
	}
}
