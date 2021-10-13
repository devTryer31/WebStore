using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Extensions;
using WebStore.Services.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = Role.Administrators)]
	public class ProductsController : Controller
	{
		private readonly IProductsAndBrandsLiteRepository _ProductsRepository;

		public ProductsController(IProductsAndBrandsLiteRepository productsRepository)
		{
			_ProductsRepository = productsRepository;
		}

		public IActionResult Index()
		{
			return View(_ProductsRepository.GetProducts().ToViewEnumerable());
		}
		//TODO: Add Remove and Edit.
	}
}
