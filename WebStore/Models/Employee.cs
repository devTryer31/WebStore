using WebStore.Models.Enums;

namespace WebStore.Models
{
	//Когда стоит использовать Record в качестве модели?

	public sealed class Employee
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Patronymic { get; set; }

		public ushort Age { get; set; }

		public uint Id { get; set; }

		public EmployeePositions Position { get; set; }

		public uint Score { get; set; }
	}
}
