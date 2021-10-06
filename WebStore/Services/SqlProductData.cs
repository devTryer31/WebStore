using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
			IQueryable<Product> products = _Db.Products
				.Include(p => p.Section)
				.Include(p => p.Brand);

			if (filter?.ProductsId is not null)
				if (filter.ProductsId.Any())
					return products.Where(p => filter.ProductsId.Contains(p.Id));

			if (filter?.BrandId is not null)
				products = products.Where(p => p.BrandId == filter.BrandId);

			if (filter?.SectionId is not null)
				products = products.Where(p => p.SectionId == filter.SectionId);

			return products;
		}

		public IEnumerable<Brand> GetBrands() => _Db.Brands;

		public IEnumerable<ProductSection> GetProductSections() => _Db.ProductSections;

		public Product GetProductById(int id) =>
			_Db.Products
				.Include(p => p.Brand)
				.Include(p => p.Section)
				.FirstOrDefault(p => p.Id == id);
	}
}
