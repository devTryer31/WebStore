using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewModels.Identity
{
    public class LoginOrRegisterViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; } = new();
        public LoginViewModel LoginViewModel { get; set; } = new();

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}