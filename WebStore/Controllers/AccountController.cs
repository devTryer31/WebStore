using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
	public class AccountController : Controller
	{
		private const string _view = "LoginOrRegister";

		private readonly UserManager<User> _UserManager;
		private readonly SignInManager<User> _SignInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_UserManager = userManager;
			_SignInManager = signInManager;
		}

		public IActionResult LoginOrRegister(string returnUrl) => View(_view,
			new LoginOrRegisterViewModel { LoginViewModel = new() { ReturnUrl = returnUrl/*Request.Headers["Referer"].ToString()*/ } });

		#region Register

		//public IActionResult Register() => View(_view, new LoginOrRegisterViewModel());

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(LoginOrRegisterViewModel model)
		{
			var loginModel = model.LoginViewModel;
			var registerModel = model.RegisterViewModel;
			if (registerModel.Name is null || registerModel.Password is null || registerModel.SamePassword is null ||
			    loginModel.Name is not null || loginModel.Password is not null)//need js lock second form while filling.
				return View(_view, model);

			User user = new() {
				UserName = registerModel.Name,
			};

			var registry_result = await _UserManager.CreateAsync(user, registerModel.Password);

			if (!registry_result.Succeeded) {
				foreach (var identityError in registry_result.Errors) {
					ModelState.AddModelError(string.Empty, identityError.Description);
				}
				return View(_view, model);
			}

			await _SignInManager.SignInAsync(user, true);
			return RedirectToAction("Index", "Home");
		}

		#endregion

		#region Login

		//public IActionResult Login(string returnUrl) => View(_view,
		//	new LoginOrRegisterViewModel { LoginViewModel = new() { ReturnUrl = returnUrl } });

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginOrRegisterViewModel model)
		{
			var loginModel = model.LoginViewModel;
			var registerModel = model.RegisterViewModel;
			if (registerModel.Name is not null || registerModel.Password is not null || registerModel.SamePassword is not null ||
			    loginModel.Name is null || loginModel.Password is null)//need js lock second form while filling.
				return View(_view, model);

			

			var login_result = await _SignInManager.PasswordSignInAsync(
				loginModel.Name,
				loginModel.Password,
				loginModel.IsRememberMe,
				false);

			if (login_result.Succeeded)
				return LocalRedirect(loginModel.ReturnUrl ?? "/"); //Check is own url or not for security.


			ModelState.AddModelError("", "Неверный логин или пароль.");
			return View(_view, model);
		}

		#endregion

		public async Task<IActionResult> Logout()
		{
			await _SignInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied() => View();
	}
}
