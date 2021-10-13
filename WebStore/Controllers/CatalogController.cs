using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Infrastructure.Extensions;
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
				.ToViewEnumerable().Take(12); //TODO: Paginate.

			return View(viewProducts);
		}

		public IActionResult Details(int id)
		{
			var product = _ProductsAndBrands.GetProductById(id);

			if (product is null)
				return NotFound();

			return View(product.ToView());
		}
	}
}
