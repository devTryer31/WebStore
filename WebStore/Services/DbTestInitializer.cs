using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;

namespace WebStore.Services
{
	public class DbTestInitializer
	{
		private readonly WebStoreDb _Db;
		private readonly ILogger _Logger;

		public DbTestInitializer(WebStoreDb db, ILogger logger = null)
		{
			_Db = db ?? throw new ArgumentNullException(nameof(db));
			_Logger = logger;
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

			await InitializeTabelsAsync();
		}

		private async Task InitializeTabelsAsync()
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
	}
}
