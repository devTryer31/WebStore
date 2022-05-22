using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;

namespace WebStore.DAL.Context
{
	public class WebStoreDb : IdentityDbContext<User, Role, string>
	{
		public DbSet<ProductSection> ProductSections { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Brand> Brands { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<Employee> Employees { get; set;}

		public WebStoreDb(DbContextOptions<WebStoreDb> options) : base(options) { }

	}
}
