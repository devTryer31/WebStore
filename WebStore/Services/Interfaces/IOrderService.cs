﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.ViewModels;

namespace WebStore.Services.Interfaces
{
    public interface IOrderService
    {
	    Task<IEnumerable<Order>> GetUserOrder(int userId);

	    Task<Order> GetOrderById(int id);

	    Task<Order> CreateOrder(string userName, CartViewModel cart, OrderViewModel orderViewModel);
    }
}
