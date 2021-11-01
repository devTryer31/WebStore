using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Entities.Orders
{
	public class Order : Entity
	{
		[Required]
		public User Сustomer { get; set; }

		[Required]
		public string Phone { get; set; }

		[Required]
		public string Address { get; set; }

		public string Description { get; set; }

		[Required]
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

		public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

		[NotMapped]
		public decimal TotalPrice => Items?.Sum(i => i.TotalPrice) ?? 0m;
	}
}
