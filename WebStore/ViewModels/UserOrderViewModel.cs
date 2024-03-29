﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
	public class UserOrderViewModel
	{
		public int Id { get; set; }

		public string Phone { get; set; }

		public string Address { get; set; }

		public decimal TotalPrice { get; set; }

		public DateTimeOffset Date { get; set; }

		public string Description { get; set; }
	}
}
