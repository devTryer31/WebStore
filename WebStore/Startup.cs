using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Services;
using WebStore.Services.InCookies;

namespace WebStore
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IRepository<Employee>, EmployeesRepository>();

			services.AddDbContext<WebStoreDb>(opt =>
			{
				opt.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
				//opt.EnableSensitiveDataLogging();
			});

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<WebStoreDb>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(opt =>
			{

#if DEBUG
				opt.Password.RequireDigit = false;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequiredLength = 3;
#endif
				opt.User.RequireUniqueEmail = false;
				opt.Lockout.AllowedForNewUsers = false;
			});

			services.ConfigureApplicationCookie(opt =>
			{
				opt.Cookie.Name = "WebStore-GV";
				opt.Cookie.HttpOnly = true;

				opt.ExpireTimeSpan = TimeSpan.FromDays(30);

				opt.LoginPath = "/Account/LoginOrRegister";
				opt.LogoutPath = "/Account/Logout";
				opt.AccessDeniedPath = "/Account/AccessDenied";

				opt.SlidingExpiration = true;//For security.
			});

			services.AddTransient<DbTestInitializer>();

			services.AddScoped<IProductsAndBrandsLiteRepository, SqlProductData>();
			services.AddScoped<ICartService, InCookiesCartService>();
			services.AddScoped<IOrderService, SqlOrderService>();

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

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{

				endpoints.MapControllerRoute(
					name: "areas",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);

				endpoints.MapControllerRoute(
					"default",
					"/{controller=Home}/{action=Index}/{id?}"
				);

			});
		}
	}
}
