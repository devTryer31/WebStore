using System.Collections.Generic;

namespace WebStore.Domain
{
	public class ProductsFilter
	{
		public int? SectionId { get; set; }

		public int? BrandId { get; set; }

		public IEnumerable<int> ProductsId { get; set; }
	}
}
