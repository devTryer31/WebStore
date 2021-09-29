using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
	public class ProductSection : NamedEntity, IOrderedEntity
	{
		public int Order { get; set; }

		/// <summary> For tree-like structure. Null if this object is root. </summary>
		public int? ParentId { get; set; }

		[ForeignKey(nameof(ParentId))]
		public ProductSection Parent { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}