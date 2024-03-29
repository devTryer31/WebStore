﻿using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Extensions;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Services.InCookies
{
	public class InCookiesCartService : ICartService
	{
		private readonly IHttpContextAccessor _HttpContextAccessor;
		private readonly IProductsAndBrandsLiteRepository _ProductsAndBrands;
		private readonly string _CartName;

		public InCookiesCartService(IHttpContextAccessor httpContextAccessor,
			IProductsAndBrandsLiteRepository productsAndBrands)
		{
			_HttpContextAccessor = httpContextAccessor;
			_ProductsAndBrands = productsAndBrands;

			var user = httpContextAccessor.HttpContext!.User;

			var username = user.Identity!.IsAuthenticated ? $"~{user.Identity.Name}" : null;

			_CartName = $"WebStore{username}.GVcart";
		}

		private Cart Cart {
			get
			{
				var context = _HttpContextAccessor.HttpContext;
				var cartFromCookies = context?.Request.Cookies[_CartName];

				var cookies = context!.Response.Cookies;

				if (cartFromCookies is null)
				{
					var cart = new Cart();
					cookies.Append(_CartName, JsonSerializer.Serialize(cart));
					return cart;
				}

				ReplaceCart(cookies, cartFromCookies);
				return JsonSerializer.Deserialize<Cart>(cartFromCookies);
			}
			set => 
				ReplaceCart(_HttpContextAccessor.HttpContext!.Response.Cookies, JsonSerializer.Serialize(value));
		}

		private void ReplaceCart(IResponseCookies cookies, string cart)
		{
			cookies.Delete(_CartName);
			cookies.Append(_CartName, cart);
		}

		public void Decrement(int id)
		{
			var cart = Cart;
			var cart_items = cart.Items;
			
			var cart_item = cart_items?.FirstOrDefault(ci => ci.ProductId == id);

			if (cart_item.Amount > 0)
				cart_item.Amount--;
			
			if(cart_item.Amount <=0)
				cart_items.Remove(cart_item);

			Cart = cart;
		}

		public void Add(int id)
		{
			var cart = Cart;

			var cart_items = cart.Items;

			var cart_item = cart_items?.FirstOrDefault(ci => ci.ProductId == id);
			if (cart_item is null)
				cart_items.Add(new CartItem
				{
					ProductId = id,
					Amount = 1
				});
			else
				cart_item.Amount++;

			Cart = cart;
		}

		public void Remove(int id)
		{
			var cart = Cart;

			var cart_items = cart.Items;
			var cart_item = cart_items?.FirstOrDefault(ci => ci.ProductId == id);

			cart_items.Remove(cart_item);

			Cart = cart;
		}

		public void Clear()
		{
			var cart = Cart;

			cart.Items.Clear();

			Cart = cart;
		}

		public CartViewModel GetViewModel()
		{
			var cart = Cart;

			if (cart.ItemsCount == 0)
				return new CartViewModel
				{
					Items = Enumerable.Empty<(ProductViewModel, int)>()
				};

			var item_to_amount_dct = cart.Items.ToDictionary(p => p.ProductId, p=>p.Amount);

			var filter = new ProductsFilter()
			{
				ProductsId = item_to_amount_dct.Keys.ToList(),
			};

			var products_in_cart = _ProductsAndBrands.GetProducts(filter).ToList();

			CartViewModel result = new()
			{
				Items = products_in_cart.Select(
					p => (p.ToView(), item_to_amount_dct[p.Id]))
			};


			return result;
		}
	}
}
