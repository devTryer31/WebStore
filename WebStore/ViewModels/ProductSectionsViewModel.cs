using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
	public class ProductSectionsViewModel
	{
		public int Id { get; set; }

		public int Order { get; set; }

		public string Name { get; set; }

		public List<ProductSectionsViewModel> Childs { get; set; } = new();
	}
}
