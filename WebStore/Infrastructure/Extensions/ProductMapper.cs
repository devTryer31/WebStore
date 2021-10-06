using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Extensions
{
	public static class ProductMapper
	{
		public static ProductViewModel ToView(this Product source) =>
			source is null
				? null
				: new ProductViewModel
				{
					Id = source.Id,
					Name = source.Name,
					Price = source.Price,
					ImgUrl = source.ImgUrl,
					Brand = source.Brand,
					Section = source.Section,
				};

		public static IEnumerable<ProductViewModel> ToViewEnumerable(this IEnumerable<Product> source) =>
			source.Select(ToView);
	}
}
