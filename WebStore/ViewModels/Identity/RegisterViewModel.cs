using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.Identity
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Имя пользователя")]
		public string Name { get; set; }
		
		[Required]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		
		[Required]
		[Display(Name = "Подтверждение пароля")]
		[Compare(nameof(Password))]
		[DataType(DataType.Password)] 
		public string SamePassword { get; set; }

	}
}
