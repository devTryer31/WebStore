using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Services
{
	public class SqlOrderService : IOrderService
	{
		private readonly WebStoreDb _Db;
		private readonly UserManager<User> _UserManager;

		public SqlOrderService(WebStoreDb db, UserManager<User> userManager)
		{
			_Db = db;
			_UserManager = userManager;
		}

		public async Task<IEnumerable<Order>> GetUserOrder(int userId)
		{
			var orders = await _Db.Orders
				.Include(o => o.Сustomer)
				.Include(o => o.Items)
				.ThenInclude(oi => oi.Product)
				.Where(o => o.Id == userId).ToArrayAsync().ConfigureAwait(false);
			return orders;
		}

		public async Task<Order> GetOrderById(int id)
		{
			return await _Db.Orders
				.Include(o => o.Сustomer)
				.Include(o => o.Items)
				.ThenInclude(oi => oi.Product)
				.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false);
		}

		public async Task<Order> CreateOrder(string userName, CartViewModel cart, OrderViewModel orderViewModel)
		{
			var user = await _UserManager.FindByNameAsync(userName).ConfigureAwait(false);
			if (user is null)
				throw new InvalidOperationException($"User with name {userName} not found.");

			await using var transaction = await _Db.Database.BeginTransactionAsync();

			var order = new Order {
				Сustomer = user,
				Phone = user.PhoneNumber,
				Address = orderViewModel.Address,
				Description = orderViewModel.Description
			};

			var cart_productsIds = cart.Items.Select(p => p.Product.Id).ToList();

			var cart_products = await _Db.Products
				.Where(p => cart_productsIds.Contains(p.Id)).ToListAsync()
				.ConfigureAwait(false);

			order.Items = cart.Items.Join(
				cart_products,
				cart_item => cart_item.Product.Id,
				prod_item => prod_item.Id,
				(c, p) => new OrderItem
				{
					Product = p,
					Price = p.Price,
					Amount = c.Amount,
					Order = order
				}).ToList();

			await _Db.Orders.AddAsync(order);

			await _Db.SaveChangesAsync();

			await transaction.CommitAsync();

			return order;
		}
	}
}
