using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WebStore.Infrastructure.Enums;
using WebStore.Models;

namespace WebStore.Services
{
	internal static class TestDataService
	{
		private static readonly List<Employee> _Data;

		static TestDataService()
		{
			//Bad practice. It must be received from appsetting.json
			const string dbPath = "Data/TextFiles/StaffData.json"; 
			if (new FileInfo(dbPath).Exists)
			{
				using var fs = new FileStream(dbPath, FileMode.Open);
				_Data = JsonSerializer.DeserializeAsync<List<Employee>>(fs).Result;
			}
			else
			{
				_Data = Enumerable.Range(1, 7).Select(x =>
					new Employee {
						Name = $"Name_{x}",
						Surname = $"Surname_{x}",
						Patronymic = $"Patronymic_{x}",
						Age = (ushort)(20 + x * 1.5),
						Id = x,
						Position = (EmployeePositions)x,
						Score = (uint)(100 + x * x)
					}).ToList();
			}
		}


		public static IEnumerable<Employee> GetEmployees => _Data;
	}
}
