using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Extensions;
using WebStore.Infrastructure.Utils.Pagination;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsAndBrandsLiteRepository _ProductsAndBrands;

        public HomeController(IProductsAndBrandsLiteRepository productsAndBrands)
        {
            _ProductsAndBrands = productsAndBrands;
        }

        public IActionResult Index(int? sectionId, int? brandId, int page = 1)
        {
            ProductsFilter filter = new()
            {
                SectionId = sectionId,
                BrandId = brandId,
            };

            var filtered = _ProductsAndBrands.GetProducts(filter).ToViewEnumerable();

            Paginator<ProductViewModel> paginator = new(filtered, filter.CountOnPage)
            {
                CurrentPage = page,
            };

            return View(paginator);
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
            Redirect("/Account/LoginOrRegister");

        public IActionResult Shop() =>
            View();
    }
}
