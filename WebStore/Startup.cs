using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Services;

namespace WebStore
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services) {
			services.AddSingleton<IRepository<Employee>, EmployeesRepository>()
				.AddSingleton<IProductsAndBrandsLiteRepository, ProductsAndBrandsLiteRepository>()
				;
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
