using Microsoft.Extensions.Logging;
using WebStore.Models;
using WebStore.Services.Base;

namespace WebStore.Services
{
	internal class EmployeesRepository : RepositoryInMemory<Employee>
	{
		private readonly ILogger<EmployeesRepository> _logger;

		public EmployeesRepository(ILogger<EmployeesRepository> logger) :base(TestDataService.GetEmployees)
		{
			_logger = logger;
		}

		protected override void UpdateItem(Employee source, Employee destination)
		{
			destination.Name = source.Name;
			destination.Surname = source.Surname;
			destination.Patronymic = source.Patronymic;
			destination.Age = source.Age;
			destination.Position = source.Position;
			destination.Score = source.Score;
		}
	}
}
