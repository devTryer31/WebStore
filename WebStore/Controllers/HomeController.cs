using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Models.Enums;

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

		public IActionResult NewAction(int id) => // http://localhost:5000/Home/NewAction/5
			Content($"New Action with id = {id}");

		//public IActionResult NewAction() => // http://localhost:5000/Home/NewAction //It's throw exception.
		//	Content("New Action without id");                                          //What solution?

		public IActionResult Staff() => //http://localhost:5000/Home/Staff
			View(__Staff);

		public IActionResult Employee(int id) =>
			View(__Staff[id]);
	}
}
