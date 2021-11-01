using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = Role.Administrators)]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
