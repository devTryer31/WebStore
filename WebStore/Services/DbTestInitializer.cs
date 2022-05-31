using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            var pendingMigrations = await _Db.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                _Logger.LogInformation("Applying migrations: {0}", string.Join(",", pendingMigrations));
                await _Db.Database.MigrateAsync(); //To the last migration.
            }

            try
            {
                await InitializeProductsAsync();
            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Error while InitializeProductsAsync.");
                throw;
            }

            try
            {
                await InitializeIdentityAsync();
            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Error while InitializeIdentityAsync.");
                throw;
            }

            try
            {
                await InitializeEmployeesAsync();
            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Error while InitializeEmployeesAsync.");
                throw;
            }
        }

        private async Task InitializeEmployeesAsync()
        {
            if(!_Db.Employees.Any())
                await _Db.Employees.AddRangeAsync(TestDataService.Employees);
            await _Db.SaveChangesAsync();
        }

        private async Task InitializeProductsAsync()
        {
            if (!_Db.Brands.Any())
                await _Db.Brands.AddRangeAsync(TestDataService.Brands);
            if (!_Db.ProductSections.Any())
                await _Db.ProductSections.AddRangeAsync(TestDataService.ProductSections);
            if (!_Db.Products.Any())
                await _Db.Products.AddRangeAsync(TestDataService.Products);

            await _Db.SaveChangesAsync();
        }

        private async Task InitializeIdentityAsync()
        {
            _Logger.LogInformation("Identity system initialize start:");


            async Task CheckRoleExistOrCreate(string roleName)
            {
                if (!await _RoleManager.RoleExistsAsync(roleName))
                {
                    _Logger.LogInformation($"Role {roleName} does not exist. Creating...");
                    await _RoleManager.CreateAsync(new Role { Name = roleName });
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
