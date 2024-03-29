﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public IActionResult LoginOrRegister(string returnUrl) => View(_view,
            new LoginOrRegisterViewModel { ReturnUrl = returnUrl });

        #region Register

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(LoginOrRegisterViewModel model)
        {
            var loginModel = model.LoginViewModel;
            var registerModel = model.RegisterViewModel;
            if (registerModel.Name is null || registerModel.Password is null || registerModel.SamePassword is null ||
                loginModel.Name is not null || loginModel.Password is not null)
                return View(_view, model);

            User user = new()
            {
                UserName = registerModel.Name,
            };

            var registry_result = await _UserManager.CreateAsync(user, registerModel.Password);

            if (!registry_result.Succeeded)
            {
                foreach (var identityError in registry_result.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError.Description);
                }
                return View(_view, model);
            }

            await _UserManager.AddToRoleAsync(user, Role.Users);
            await _SignInManager.SignInAsync(user, true);
            return LocalRedirect(model.ReturnUrl ?? "/");
        }

        #endregion

        #region Login

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginOrRegisterViewModel model)
        {
            var loginModel = model.LoginViewModel;
            var registerModel = model.RegisterViewModel;
            if (registerModel.Name is not null || registerModel.Password is not null || registerModel.SamePassword is not null ||
                loginModel.Name is null || loginModel.Password is null)
            {
                ModelState.AddModelError("LoginOrRegisterModelValueFieldError", $"Some info not correct for type {typeof(LoginOrRegisterViewModel)}.");
                return View(_view, model);
            }



            var login_result = await _SignInManager.PasswordSignInAsync(
                loginModel.Name,
                loginModel.Password,
                loginModel.IsRememberMe,
                false);

            if (login_result.Succeeded)
                return LocalRedirect(model.ReturnUrl ?? "/");


            ModelState.AddModelError("", "Неверный логин или пароль.");
            return View(_view, model);
        }

        #endregion

        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _SignInManager.SignOutAsync();
            return LocalRedirect(returnUrl ?? "/");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}
