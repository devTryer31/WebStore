using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Enums;

namespace WebStore.ViewModels
{
	public class EmployeeViewModel
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Patronymic { get; set; }

		public ushort Age { get; set; }

		public int Id { get; set; }

		public EmployeePositions Position { get; set; }

		public uint Score { get; set; }
	}
}
