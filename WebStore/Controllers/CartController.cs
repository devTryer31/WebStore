﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService _CartService;

		public CartController(ICartService cartService)
		{
			_CartService = cartService;
		}

		public IActionResult Index() => View(new CartAndOrderViewModel() {
			Cart = _CartService.GetViewModel()
		});

		public IActionResult Add(int id)
		{
			_CartService.Add(id);
			return RedirectToAction("Index");
		}

		public IActionResult Decrement(int id)
		{
			_CartService.Decrement(id);
			return RedirectToAction("Index");
		}

		public IActionResult Remove(int id)
		{
			_CartService.Remove(id);
			return RedirectToAction("Index");
		}

		public IActionResult Clear()
		{
			_CartService.Clear();
			return RedirectToAction("Index");
		}

		[Authorize]
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> CheckOut(OrderViewModel orderModel, [FromServices] IOrderService orderService)
		{
			if (!ModelState.IsValid)
				return View(new CartAndOrderViewModel() {
					Cart = _CartService.GetViewModel(),
					Order = orderModel
				});

			var order = await orderService.CreateOrderAsync(User.Identity!.Name, _CartService.GetViewModel(), orderModel);

			_CartService.Clear();

			return RedirectToAction(nameof(ConfirmedOrder), new { Id = order.Id });
		}

		public IActionResult ConfirmedOrder(int id)
		{
			return View();
		}
	}
}
