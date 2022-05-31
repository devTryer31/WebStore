using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Extensions
{
    public static class EmployeeMapper
    {
        public static EmployeeViewModel ToViewModel(this Employee empl)
            => empl is null
            ? null
            : new EmployeeViewModel()
            {
                Id = empl.Id,
                Name = empl.Name,
                Surname = empl.Surname,
                Patronymic = empl.Patronymic ?? "No patronymic",
                Age = empl.Age,
                Score = empl.Score,
                Position = Enum.GetName(empl.Position),
            };

        public static IEnumerable<EmployeeViewModel> ToViewModelEnumerable(this IEnumerable<Employee> empls)
            => empls.Select(e => e.ToViewModel());
    }
}
