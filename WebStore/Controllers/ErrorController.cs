using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Controllers
{
	public class ErrorController : Controller
	{
		public new ActionResult NotFound()
		{
			Response.StatusCode = 404;
			return View();
		}
   }
}
