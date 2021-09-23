using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebStore.Infrastructure.Enums;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly IRepository<Employee> _EmployeesData;
		private readonly ILogger<EmployeesController> _Logger;

		public EmployeesController(IRepository<Employee> EmployeesData, ILogger<EmployeesController> Logger)
		{
			_EmployeesData = EmployeesData;
			_Logger = Logger;
		}

		public IActionResult Index() => View(_EmployeesData.GetAll());

		public IActionResult Info(int? id)
		{
			if (!id.HasValue)
				return NotFound();

			var employee = _EmployeesData.Get(id.Value);

			if (employee is null)
				return NotFound();

			return View(employee);
		}

		public IActionResult Add() => View();

		#region Edit

		public IActionResult Edit(int id)
		{
			var employee = _EmployeesData.Get(id);
			if (employee is null) return NotFound();
			EmployeeViewModel viewModel = new EmployeeViewModel
			{
				Name = employee.Name,
				Surname = employee.Surname,
				Patronymic = employee.Patronymic,
				Age = employee.Age,
				Id = employee.Id,
				Position = employee.Position,
				Score = employee.Score,
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Edit(EmployeeViewModel viewModel)
		{
			Employee employee = new Employee
			{
				Name = viewModel.Name,
				Surname = viewModel.Surname,
				Patronymic = viewModel.Patronymic,
				Age = viewModel.Age,
				Id = viewModel.Id,
				Position = viewModel.Position,
				Score = viewModel.Score,
			};

			_EmployeesData.Update(employee);
			return RedirectToAction(nameof(Index));
		}

		#endregion

		public IActionResult Remove() => View();
	}
}
