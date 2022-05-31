namespace WebStore.Domain.Entities
{
    public sealed class Employee : Base.NamedEntity
    {
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public ushort Age { get; set; }

        public EmployeePositions Position { get; set; }

        public uint Score { get; set; }
    }

    public enum EmployeePositions : byte
    {
        Boss = 0,
        Manager,
        Programmer,
        QaEngineer,
        Cleaner
    }
}
