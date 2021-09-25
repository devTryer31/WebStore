using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces
{
	internal interface IProductsAndBrandsLiteRepository
	{
		public IEnumerable<Product> GetProducts();

		public IEnumerable<Brand> GetBrands();

		public IEnumerable<ProductSection> GetProductSections();
	}
}
