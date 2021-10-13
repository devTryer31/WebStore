using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces
{
	public interface IProductsAndBrandsLiteRepository
	{
		public IEnumerable<Product> GetProducts(ProductsFilter filter);

		public IEnumerable<Brand> GetBrands();

		public IEnumerable<ProductSection> GetProductSections();
		
		Product GetProductById(int id);
	}
}
