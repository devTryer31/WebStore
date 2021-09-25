using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
	public class BrandsViewComponent : ViewComponent
	{
		private readonly IProductsAndBrandsLiteRepository _ProductsAndBrands;

		public BrandsViewComponent(IProductsAndBrandsLiteRepository productsAndBrandsLiteRepository)
		{
			_ProductsAndBrands = productsAndBrandsLiteRepository;
		}

		public IViewComponentResult Invoke() => View(_ProductsAndBrands.GetBrands().
			OrderBy(b => b.Order).Select(b => new BrandViewModel
			{
				Name = b.Name,
				PositionsAmount = b.PositionsAmount,
				Order = b.Order,
				Id = b.Id,
			}));
	}
}
