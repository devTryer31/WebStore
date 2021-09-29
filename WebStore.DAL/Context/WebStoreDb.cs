using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
	public class WebStoreDb : DbContext
	{
		public DbSet<ProductSection> ProductSections { get; set; }
		
		public DbSet<Product> Products { get; set; }
		
		public DbSet<Brand> Brands { get; set; }

		public WebStoreDb(DbContextOptions<WebStoreDb> options) :base(options) {}

	}
}
