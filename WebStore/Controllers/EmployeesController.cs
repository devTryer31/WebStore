using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
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
		private readonly IConfiguration _Configuration;

		public EmployeesController(IRepository<Employee> EmployeesData, ILogger<EmployeesController> Logger, IConfiguration Configuration)
		{
			_EmployeesData = EmployeesData;
			_Logger = Logger;
			_Configuration = Configuration;
		}

		private async void SaveChanges()
		{
			await using FileStream fs = new(_Configuration["EmployeesDbFilePath"], FileMode.Create);
			await JsonSerializer.SerializeAsync(fs, new List<Employee>(_EmployeesData.GetAll()));
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

		//public IActionResult Add() => View(); // in /Edit/[id = null].

		#region Edit

		public IActionResult Edit(int? id)
		{
			if (id is null) // If we adding new employee.
				return View(new EmployeeViewModel()); //Fill the empty ViewModel.

			var employee = _EmployeesData.Get(id.Value);
			if (employee is null)
				return NotFound();
			EmployeeViewModel viewModel = new EmployeeViewModel {
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
			Employee employee = new Employee {
				Name = viewModel.Name,
				Surname = viewModel.Surname,
				Patronymic = viewModel.Patronymic,
				Age = viewModel.Age,
				Id = viewModel.Id,
				Position = viewModel.Position,
				Score = viewModel.Score,
			};

			if (employee.Id == 0) // If we adding new employee.
				_EmployeesData.Add(employee);
			else
				_EmployeesData.Update(employee);

			SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		#endregion

		#region Remove actions

		public IActionResult Remove(int id)
		{
			if (id < 0)
				return BadRequest();

			var employee = _EmployeesData.Get(id);
			if (employee is null)
				return NotFound();

			var vm = new EmployeeViewModel {
				Name = employee.Name,
				Surname = employee.Surname,
				Patronymic = employee.Patronymic,
				Age = employee.Age,
				Id = employee.Id,
				Position = employee.Position,
				Score = employee.Score,
			};

			return View(vm);
		}

		[HttpPost]
		public IActionResult RemoveConfirmed(int id)
		{
			_EmployeesData.Remove(_EmployeesData.Get(id));

			SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		#endregion
	}
}
