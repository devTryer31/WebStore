using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
	public class ErrorController : Controller
	{
		[Route("Error/Index/{errorCode:int}")]
		public IActionResult Index(int errorCode) =>
			errorCode switch {
				404 => RedirectToAction(nameof(NotFound)),
				401 => Content($"You must be authorized for this action. Status code: {errorCode}"),
				_ => Content($"Error code: {errorCode}. Sorry :("),
			};

		public new IActionResult NotFound()
		{
			return View();
		}
	}
}
