using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Entities
{
	public class Product : NamedEntity, IOrderedEntity
	{
		public int Order { get; set; }
		
		public int SectionId { get; set; }

		[ForeignKey(nameof(SectionId))]
		public ProductSection Section { get; set; }

		public int? BrandId { get; set; }

		[ForeignKey(nameof(BrandId))]
		public Brand Brand { get; set; }

		public string ImgUrl { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

        public ICollection<User> UsersMarkedFavorite { get; set; }
    }
}
