using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class ProductViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImgUrl { get; set; }

        public Brand Brand { get; set; }

        public ProductSection Section { get; set; }

        [HiddenInput]
        public bool IsUserFavorite { get; set; } = false;
    }
}
