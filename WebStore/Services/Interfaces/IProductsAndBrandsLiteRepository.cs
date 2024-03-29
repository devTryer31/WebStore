﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces
{
	public interface IProductsAndBrandsLiteRepository
	{
		public IEnumerable<Product> GetProducts(ProductsFilter filter = null);

		public IEnumerable<Brand> GetBrands();

		public IEnumerable<ProductSection> GetProductSections();

		public void AddProduct(Product source); 
		
		Product GetProductById(int id);

		int GetProductCount();
	}
}
