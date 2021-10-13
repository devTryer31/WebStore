using System;
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
	    Task<IEnumerable<Order>> GetUserOrderAsync(int userId);

	    Task<Order> GetOrderByIdAsync(int id);

	    Task<Order> CreateOrderAsync(string userName, CartViewModel cart, OrderViewModel orderViewModel);
    }
}
