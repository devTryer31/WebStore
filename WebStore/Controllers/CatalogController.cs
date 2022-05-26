using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Infrastructure.Extensions;
using WebStore.Infrastructure.Utils.Pagination;
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

        public IActionResult Index(int? sectionId, int? brandId, int page = 1)//TODO: Equal with HomeController.Index method
        {
            ProductsFilter filter = new()
            {
                SectionId = sectionId,
                BrandId = brandId,
            };

            var filtered = _ProductsAndBrands.GetProducts(filter).ToViewEnumerable();

            Paginator<ProductViewModel> paginator = new(filtered, filter.CountOnPage)
            {
                CurrentPage = page
            };

            return View(paginator);
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
