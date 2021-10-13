﻿using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
	    private readonly ICartService _CartService;

	    public CartController(ICartService cartService)
	    {
		    _CartService = cartService;
	    }

	    public IActionResult Index() => View(_CartService.GetViewModel());

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
    }
}
