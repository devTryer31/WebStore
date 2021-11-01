using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewModels.Identity
{
	public class LoginViewModel
	{
		[Required]
		[Display(Name = "Имя пользователя")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня")]
		public bool IsRememberMe { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ReturnUrl
		{
			get;
			set;
			//get
			//{
			//	Console.WriteLine($"get [{_ReturnUrl}]");
			//	return _ReturnUrl;
			//}
			//set
			//{
			//	Console.WriteLine($"set [{value}]");
			//	_ReturnUrl = value;
			//}
		}
	}
}