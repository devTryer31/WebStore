using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
	public class ProductSectionsViewComponent : ViewComponent
	{
		private readonly IProductsAndBrandsLiteRepository _ProductsAndBrands;

		public ProductSectionsViewComponent( IProductsAndBrandsLiteRepository productsAndBrandsLiteRepository)
		{
			_ProductsAndBrands = productsAndBrandsLiteRepository;
		}

		public IViewComponentResult Invoke()
		{
			var sections = _ProductsAndBrands.GetProductSections();

			var parentSections = sections.Where(s => s.ParentId is null).ToList();

			return View(parentSections.Select(ps => new ProductSectionsViewModel
				{
					Id = ps.Id,
					Order = ps.Order,
					Name = ps.Name,
					Childs = sections.Where(s => s.ParentId == ps.Id)
													.Select(cs => new ProductSectionsViewModel
													{
														Id = cs.Id,
														Order = cs.Order,
														Name = cs.Name,
														Childs = null
													})
													.OrderBy(s => s.Order)
													.ToList()
													

				}
				).OrderBy(s => s.Order)
			);
		}
	}
}
