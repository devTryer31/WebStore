using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        private static readonly IEnumerable<SelectListItem> _avaliablePositionsNames = 
            System.Enum.GetNames<EmployeePositions>().Select(n => new SelectListItem { Value = n, Text = n});

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        [Range(18, 100, ErrorMessage = "Age not correct. Must be between 18 and 100")]
        public ushort Age { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public uint Score { get; set; }

        public IEnumerable<SelectListItem> AvaliablePositions => _avaliablePositionsNames;
    }
}
