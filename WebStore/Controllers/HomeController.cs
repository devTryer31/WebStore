using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Infrastructure.Enums;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductsAndBrandsLiteRepository _ProductsAndBrands;

		public HomeController( IProductsAndBrandsLiteRepository productsAndBrands)
		{
			_ProductsAndBrands = productsAndBrands;
		}

		public IActionResult Index(int? sectionId, int? brandId)
		{
			ProductsFilter filter = new() {
				SectionId = sectionId,
				BrandId = brandId
			};

			var viewProducts = _ProductsAndBrands.GetProducts(filter)
				.Select(p => new ProductViewModel {
					Id = p.Id,
					Name = p.Name,
					Price = p.Price,
					ImgUrl = p.ImgUrl,

				}).Take(6);

			return View(viewProducts);
		}

		public IActionResult BlogSingle() =>
			View();

		public IActionResult BlogsList() =>
			View();
		
		public IActionResult Cart() =>
			View();
		
		public IActionResult Checkout() =>
			View();
		
		public IActionResult ContactUs() =>
			View();
		
		public IActionResult Login() =>
			View();
		
		public IActionResult ProductDetails() =>
			View();
		
		public IActionResult Shop() =>
			View();
	}
}
