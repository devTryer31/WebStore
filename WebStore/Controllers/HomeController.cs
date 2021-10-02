﻿using System.Collections.Generic;
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
				Id = i,
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
		
		public IActionResult ProductDetails() =>
			View();
		
		public IActionResult Shop() =>
			View();
	}
}
