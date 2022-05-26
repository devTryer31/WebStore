using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Areas.Admin.Models.ViewModels
{
    public class ProductViewModel
    {
		public int Id { get; }

        [Required]
		public string Name { get; set; }

        [Required]
		public decimal Price { get; set; }

        [Required]
		public string ImgUrl { get; set; }
//TODO: Implement
		//public Brand Brand { get; set; } 

		//public ProductSection Section { get; set; }
	}
}
