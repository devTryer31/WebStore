using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Description { get; set; }
    }
}
