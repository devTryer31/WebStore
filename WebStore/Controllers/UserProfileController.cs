using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
	[Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

	    public async Task<IActionResult> Index()
        {
			var currentUser = await _userManager.GetUserAsync(HttpContext.User);

			UserProfileViewModel vm = new()
            {
				UserName = currentUser.UserName,
				Email = currentUser.Email,
				PhoneNumber = currentUser.PhoneNumber,
            };

			return View(vm);
        }
	    
	    
	    public async Task<IActionResult> Orders([FromServices] IOrderService orderService)
	    {
		    var orders = await orderService.GetUserOrdersAsync(User.Identity!.Name);

		    return View(orders.Select(o => new UserOrderViewModel
		    {
			    Id = o.Id,
			    Phone = o.Phone,
			    Address = o.Address,
			    TotalPrice = o.TotalPrice,
			    Date = o.OrderDate,
			    Description = o.Description
		    }));
	    }
    }
}
