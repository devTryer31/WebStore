using WebStore.Infrastructure.Enums;
using WebStore.Models.Interfaces;

namespace WebStore.Models
{
	public sealed class Employee : IEntity
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
