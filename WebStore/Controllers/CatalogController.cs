using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
	public class CatalogController : Controller
	{
		private readonly IProductsAndBrandsLiteRepository _ProductsAndBrands;

		public CatalogController(IProductsAndBrandsLiteRepository productsAndBrands)
		{
			_ProductsAndBrands = productsAndBrands;
		}

		public IActionResult Index(int? sectionId, int? brandId)
		{
			ProductsFilter filter = new()
			{
				SectionId = sectionId,
				BrandId = brandId
			};

			var viewProducts = _ProductsAndBrands.GetProducts(filter)
				.Select(p => new ProductViewModel
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price,
					ImgUrl = p.ImgUrl,

				}).Take(12); //TODO: Paginate.

			return View(viewProducts);
		}
	}
}
