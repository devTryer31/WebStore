using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Enums;
using WebStore.Models;

namespace WebStore.Controllers
{
	public class HomeController : Controller
	{

		private static readonly List<Employee> __Staff =
			Enumerable.Range(0, 5).Select(i => new Employee {
				Name = $"Name_{i}",
				Surname = $"Surname_{i}",
				Patronymic = $"Patronymic_{i}",
				Age = (ushort)(20 + 5 * i),
				Id = (uint)i,
				Position = (EmployeePositions)i,
				Score = (uint)(20 + 17 * i)
			}).ToList();

		public IActionResult Index() =>
			View();

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
			View();


		public IActionResult Staff() => //http://localhost:5000/Home/Staff
			View(__Staff);

		public IActionResult Employee(int? id)
		{
			if (!id.HasValue)
				return Content("The employee id is not specified.");

			if (id >= __Staff.Count || id < 0)
				return Content("Employee not found.");
			return View(__Staff[id.Value]);
		}
	}
}
