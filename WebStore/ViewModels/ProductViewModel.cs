﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public string ImgUrl { get; set; }

		public Brand Brand { get; set; }

		public ProductSection Section { get; set; }
	}
}
