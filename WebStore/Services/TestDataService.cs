using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Enums;
using WebStore.Models;

namespace WebStore.Services
{
	internal static class TestDataService
	{
		private static readonly List<Employee> _Data =
			Enumerable.Range(0, 5).Select(x =>
				new Employee
				{
					Name = $"Name_{x}",
					Surname = $"Surname_{x}",
					Patronymic = $"Patronymic_{x}",
					Age = (ushort) x,
					Id = x,
					Position = (EmployeePositions)x,
					Score = (uint) (100+x*x)
				}).ToList();

		public static IEnumerable<Employee> GetEmployees => _Data;
	}
}
