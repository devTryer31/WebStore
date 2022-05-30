using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Controllers
{
    [Authorize]
    public class FavoriteProductsController : Controller
    {
        private readonly WebStoreDb _db;

        public FavoriteProductsController(WebStoreDb db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult> AddToFavorite([FromBody] int productId)
        {
            var user = await _db.Users.Include(u => u.FavoriteProducts)
                .FirstOrDefaultAsync(u => u.UserName == HttpContext.User.Identity.Name);

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (user is null || product is null)
                return NotFound(new { err = "User or product not found." });

            user.FavoriteProducts.Add(product);

            _db.Update(user);

            await _db.SaveChangesAsync();

            return Ok(productId);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFromFavorite([FromBody] int productId)
        {
            var user = await _db.Users.Include(u => u.FavoriteProducts)
                .FirstOrDefaultAsync(u => u.UserName == HttpContext.User.Identity.Name);

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (user is null || product is null)
                return NotFound(new { err = "User or product not found." });

            user.FavoriteProducts.Remove(product);

            _db.Update(user);

            await _db.SaveChangesAsync();

            return Ok(productId);
        }
    }
}
