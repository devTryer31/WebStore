using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		public ICollection<Order> Items { get; set; } = new List<Order>();

	}
}
