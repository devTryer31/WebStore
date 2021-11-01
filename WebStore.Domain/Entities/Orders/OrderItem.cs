using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities.Orders
{
	public class OrderItem : Entity
	{
		[Required]
		public Product Product { get; set; }

		//After discounts, extra charges, etc.
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		public int Amount { get; set; }

		public Order Order { get; set; }

		[NotMapped]
		public decimal TotalPrice => Price * Amount;
	}
}