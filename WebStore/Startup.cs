using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Services;

namespace WebStore
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		
		public void ConfigureServices(IServiceCollection services) {
			services.AddSingleton<IRepository<Employee>, EmployeesRepository>()
				.AddSingleton<IProductsAndBrandsLiteRepository, ProductsAndBrandsLiteRepository>()
				.AddDbContext<WebStoreDb>(opt => 
					opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
			services.AddControllersWithViews()
				.AddRazorRuntimeCompilation()
				;
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseStatusCodePagesWithReExecute("/Error/Index/{0}");
			app.UseStaticFiles();
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					"default",
					"/{controller=Home}/{action=Index}/{id?}"
				);
			});
		}
	}
}
