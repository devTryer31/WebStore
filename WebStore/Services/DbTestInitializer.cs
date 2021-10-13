using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Services
{
	public class DbTestInitializer
	{
		private readonly WebStoreDb _Db;
		private readonly ILogger<DbTestInitializer> _Logger;
		private readonly UserManager<User> _UserManager;
		private readonly RoleManager<Role> _RoleManager;

		public DbTestInitializer(WebStoreDb db, ILogger<DbTestInitializer> logger,
			UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			_Db = db ?? throw new ArgumentNullException(nameof(db));
			_Logger = logger;
			_UserManager = userManager;
			_RoleManager = roleManager;
		}

		public async Task InitializeAsync()
		{
			//bool db_deleted = await _Db.Database.EnsureDeletedAsync();
			//bool db_created = await _Db.Database.EnsureCreatedAsync();

			//if(db_created)


			var pendingMigrations = await _Db.Database.GetPendingMigrationsAsync();
			//var appliedMigrations = await _Db.Database.GetAppliedMigrationsAsync();

			if (pendingMigrations.Any()) {
				_Logger.LogInformation("Applying migrations: {0}", string.Join(",", pendingMigrations));
				await _Db.Database.MigrateAsync(); //To the last migration.
			}

			try {
				await InitializeProductsAsync();
			}
			catch (Exception e) {
				_Logger.LogError(e, "Error while InitializeProductsAsync.");
				throw;
			}

			try {
				await InitializeIdentityAsync();
			}
			catch (Exception e) {
				_Logger.LogError(e, "Error while InitializeIdentityAsync.");
				throw;
			}


		}

		private async Task InitializeProductsAsync()
		{
			//var tablesFillTasks = new List<Task>(_Db.Model.GetEntityTypes().Count());

			if (!_Db.Brands.Any()) {
				//	tablesFillTasks.Add(Task.Run(async () =>
				//		{
				await using (await _Db.Database.BeginTransactionAsync()) {
					_Db.Brands.AddRange(TestDataService.Brands);

					await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
					await _Db.SaveChangesAsync();
					await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");
					await _Db.Database.CommitTransactionAsync();
				}
				//	}
				//));
				//await tablesFillTasks[taskId].ConfigureAwait(false);
			}
			if (!_Db.Products.Any()) {
				//	tablesFillTasks.Add(Task.Run(async () =>
				//		{
				await using (await _Db.Database.BeginTransactionAsync()) {
					_Db.Products.AddRange(TestDataService.Products);

					await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
					await _Db.SaveChangesAsync();
					await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");
					await _Db.Database.CommitTransactionAsync();
				}
				//	}
				//));
				//await tablesFillTasks[taskId].ConfigureAwait(false);
			}

			if (!_Db.ProductSections.Any()) {
				//	tablesFillTasks.Add(Task.Run(async () =>
				//	  {
				await using (await _Db.Database.BeginTransactionAsync()) {
					_Db.ProductSections.AddRange(TestDataService.ProductSections);

					await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[ProductSections] ON");
					await _Db.SaveChangesAsync();
					await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[ProductSections] OFF");
					await _Db.Database.CommitTransactionAsync();
				}
				//}
				//));
				//await tablesFillTasks[taskId].ConfigureAwait(false);
			}
			//tablesFillTasks.ForEach(t => t.Wait());
		}

		private async Task InitializeIdentityAsync()
		{
			_Logger.LogInformation("Identity system initialize start:");


			async Task CheckRoleExistOrCreate(string roleName)
			{
				if (!await _RoleManager.RoleExistsAsync(roleName))
				{
					_Logger.LogInformation($"Role {roleName} does not exist. Creating...");
					await _RoleManager.CreateAsync(new Role {Name = roleName});
					_Logger.LogInformation($"Role {roleName} created!");
				}
				else
				{
					_Logger.LogInformation($"Role {roleName} is already exist.");
				}
			}

			await CheckRoleExistOrCreate(Role.Administrators);
			await CheckRoleExistOrCreate(Role.Users);


			if (await _UserManager.FindByNameAsync(User.Administrator) is null)
			{
				_Logger.LogInformation("The admin-user does not exist. Creating...");
				var admin = new User()
				{
					UserName = User.Administrator
				};

				var creating_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);

				if (creating_result.Succeeded)
				{
					_Logger.LogInformation("Admin-user created!");
					await _UserManager.AddToRoleAsync(admin, Role.Administrators);
					_Logger.LogInformation($"Admin-user got the administrator role.");
				}
				else
				{
					var errors = string.Join(", ", creating_result.Errors);
					_Logger.LogError("Admin-user does not created. Errors: {0}", errors);

					throw new InvalidOperationException($"Cannot create admin-user. Errors: {errors}");
				}
				
				_Logger.LogInformation("End admin-user creating.");
			}

			_Logger.LogInformation("End identity system initialize.");
		}
	}
}
