using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Extensions;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = Role.Administrators)]
	public class ProductsController : Controller
	{
		private readonly IProductsAndBrandsLiteRepository _ProductsRepository;
		private readonly WebStoreDb _Db;

		public ProductsController(IProductsAndBrandsLiteRepository productsRepository, WebStoreDb db)
		{
			_ProductsRepository = productsRepository;
			_Db = db;
		}

		public IActionResult Index() =>
			View(_ProductsRepository.GetProducts().ToViewEnumerable());

		#region Edit




		public IActionResult Edit(int? id)
		{
			if (id is null) //For add new product.
				return View(new ProductViewModel());

			var productVM = _ProductsRepository.GetProductById(id.Value)?.ToView();

			if (productVM is null)
				return NotFound();

			return View(productVM);

		}

		[HttpPost]
		public IActionResult Edit(ProductViewModel productModel)
		{
			Product product = new Product //TODO: Make ProductViewModel for administrator with all fields.
			{
				Id = productModel.Id,
				Name = productModel.Name,
				ImgUrl = productModel.ImgUrl,
				Price = productModel.Price
			};

			if (product.Id == 0) //If new product.
			{
				product.Section = _Db.ProductSections.First(); //TODO: Add choosing sections.
				_ProductsRepository.AddProduct(product);
				return RedirectToAction("Index");
			}

			_Db.Products.Update(product).State = EntityState.Modified; //I think it pointless.
			_Db.SaveChanges();

			return RedirectToAction("Index");
		}

		#endregion


		#region Delete

		public IActionResult Delete(int id)
		{
			var productVM = _Db.Products.FirstOrDefaultAsync(p => p.Id == id).Result?.ToView(); //Bad practice.

			if (productVM is null)
				return NotFound();

			return View("Delete", productVM);
		}

		[HttpPost]
		public IActionResult Delete(ProductViewModel productView)
		{

			var product = _Db.Products.FirstOrDefaultAsync(p => p.Id == productView.Id).Result; //Bad practice.

			if (product is null)
				return NotFound();

			_Db.Products.Remove(product);
			_Db.SaveChanges();

			return RedirectToAction("Index");
		}

		#endregion
	}
}
