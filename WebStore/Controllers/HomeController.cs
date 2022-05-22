using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Infrastructure.Extensions;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsAndBrandsLiteRepository _ProductsAndBrands;

        public HomeController(IProductsAndBrandsLiteRepository productsAndBrands)
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
                .ToViewEnumerable().Take(6);

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
            Redirect("/Account/LoginOrRegister");

        public IActionResult ProductDetails() =>
            View();

        public IActionResult Shop() =>
            View();
    }
}
