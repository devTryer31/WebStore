using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Extensions;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DbSet<Employee> _EmployeesData;
        private readonly WebStoreDb _db;
        private readonly ILogger<EmployeesController> _Logger;
        private readonly IConfiguration _Configuration;

        public EmployeesController(WebStoreDb db, ILogger<EmployeesController> Logger, IConfiguration Configuration)
        {
            _EmployeesData = db.Employees;
            _db = db;
            _Logger = Logger;
            _Configuration = Configuration;
        }

        public IActionResult Index() => View(_EmployeesData.ToViewModelEnumerable());

        public async Task<IActionResult> Info(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var employee = await _EmployeesData.FindAsync(id.Value); //_EmployeesData.Get(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee.ToViewModel());
        }

        #region Edit
        [Authorize(Roles = Role.Administrators)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) // If we adding new employee.
                return View(new EmployeeViewModel()); //Fill the empty ViewModel.

            var employee = await _EmployeesData.FindAsync(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee.ToViewModel());
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrators)]
        public async Task<IActionResult> Edit(EmployeeViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return View(viewModel);

            Employee employee = new Employee
            {
                Name = viewModel.Name,
                Surname = viewModel.Surname,
                Patronymic = viewModel.Patronymic,
                Age = viewModel.Age,
                Id = viewModel.Id,
                Position = Enum.Parse<EmployeePositions>(viewModel.Position),
                Score = viewModel.Score,
            };

            if (employee.Id == 0) // If we adding new employee.
                _EmployeesData.Add(employee);
            else
                _EmployeesData.Update(employee);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Remove actions
        [Authorize(Roles = Role.Administrators)]
        public async Task<IActionResult> Remove(int id)
        {
            if (id < 0)
                return BadRequest();

            var employee = await _EmployeesData.FindAsync(id);
            if (employee is null)
                return NotFound();

            return View(employee.ToViewModel());
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrators)]
        public async Task<IActionResult> Remove(EmployeeViewModel vm)
        {
            _EmployeesData.Remove(new Employee { Id = vm.Id });

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
