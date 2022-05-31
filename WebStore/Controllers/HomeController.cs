using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Extensions;
using WebStore.Infrastructure.Utils.Pagination;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsAndBrandsLiteRepository _ProductsAndBrands;
        private readonly UserManager<User> _userManager;

        public HomeController(IProductsAndBrandsLiteRepository productsAndBrands, UserManager<User> userManager)
        {
            _ProductsAndBrands = productsAndBrands;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? sectionId, int? brandId, int page = 1)
        {
            ProductsFilter filter = new()
            {
                SectionId = sectionId,
                BrandId = brandId,
            };

            var filtered = _ProductsAndBrands.GetProducts(filter).ToViewEnumerable().ToList();
            var user = await _userManager.Users.Include(u => u.FavoriteProducts)
                .FirstOrDefaultAsync(u => u.UserName == HttpContext.User.Identity.Name);

            if (user is not null)
                foreach (var product in filtered)
                {
                    if (user.FavoriteProducts.Any(p => p.Id == product.Id))
                        product.IsUserFavorite = true;
                }

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
