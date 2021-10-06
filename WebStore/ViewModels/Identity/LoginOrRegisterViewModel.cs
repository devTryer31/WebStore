namespace WebStore.ViewModels.Identity
{
	public class LoginOrRegisterViewModel
	{
		public RegisterViewModel RegisterViewModel { get; set; } = new();
		public LoginViewModel LoginViewModel { get; set; } = new();
	}
}