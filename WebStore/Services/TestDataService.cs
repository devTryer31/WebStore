using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Enums;
using WebStore.Models;

namespace WebStore.Services
{
	internal static class TestDataService
	{
		private static readonly List<Employee> _Data;

		static TestDataService()
		{
			//Bad practice. It must be received from appsetting.json
			const string fileDbPath = "Data/TextFiles/StaffData.json";
			if (new FileInfo(fileDbPath).Exists)
			{
				using var fs = new FileStream(fileDbPath, FileMode.Open);
				_Data = JsonSerializer.DeserializeAsync<List<Employee>>(fs).Result;
			}
			else
			{
				_Data = Enumerable.Range(1, 7).Select(x =>
					new Employee
					{
						Name = $"Name_{x}",
						Surname = $"Surname_{x}",
						Patronymic = $"Patronymic_{x}",
						Age = (ushort) (20 + x * 1.5),
						Id = x,
						Position = (EmployeePositions) x,
						Score = (uint) (100 + x * x)
					}).ToList();
			}
		}

		public static IEnumerable<Employee> GetEmployees => _Data; //Bad naming. Just 'Employees' required.

      public static IEnumerable<ProductSection> ProductSections { get; } = new[]
{
             new ProductSection { Id = 1, Name = "Спорт", Order = 0 },
              new ProductSection { Id = 2, Name = "Nike", Order = 0, ParentId = 1 },
              new ProductSection { Id = 3, Name = "Under Armour", Order = 1, ParentId = 1 },
              new ProductSection { Id = 4, Name = "Adidas", Order = 2, ParentId = 1 },
              new ProductSection { Id = 5, Name = "Puma", Order = 3, ParentId = 1 },
              new ProductSection { Id = 6, Name = "ASICS", Order = 4, ParentId = 1 },
              new ProductSection { Id = 7, Name = "Для мужчин", Order = 1 },
              new ProductSection { Id = 8, Name = "Fendi", Order = 0, ParentId = 7 },
              new ProductSection { Id = 9, Name = "Guess", Order = 1, ParentId = 7 },
              new ProductSection { Id = 10, Name = "Valentino", Order = 2, ParentId = 7 },
              new ProductSection { Id = 11, Name = "Диор", Order = 3, ParentId = 7 },
              new ProductSection { Id = 12, Name = "Версачи", Order = 4, ParentId = 7 },
              new ProductSection { Id = 13, Name = "Армани", Order = 5, ParentId = 7 },
              new ProductSection { Id = 14, Name = "Prada", Order = 6, ParentId = 7 },
              new ProductSection { Id = 15, Name = "Дольче и Габбана", Order = 7, ParentId = 7 },
              new ProductSection { Id = 16, Name = "Шанель", Order = 8, ParentId = 7 },
              new ProductSection { Id = 17, Name = "Гуччи", Order = 9, ParentId = 7 },
              new ProductSection { Id = 18, Name = "Для женщин", Order = 2 },
              new ProductSection { Id = 19, Name = "Fendi", Order = 0, ParentId = 18 },
              new ProductSection { Id = 20, Name = "Guess", Order = 1, ParentId = 18 },
              new ProductSection { Id = 21, Name = "Valentino", Order = 2, ParentId = 18 },
              new ProductSection { Id = 22, Name = "Dior", Order = 3, ParentId = 18 },
              new ProductSection { Id = 23, Name = "Versace", Order = 4, ParentId = 18 },
              new ProductSection { Id = 24, Name = "Для детей", Order = 3 },
              new ProductSection { Id = 25, Name = "Мода", Order = 4 },
              new ProductSection { Id = 26, Name = "Для дома", Order = 5 },
              new ProductSection { Id = 27, Name = "Интерьер", Order = 6 },
              new ProductSection { Id = 28, Name = "Одежда", Order = 7 },
              new ProductSection { Id = 29, Name = "Сумки", Order = 8 },
              new ProductSection { Id = 30, Name = "Обувь", Order = 9 },
        };

      public static IEnumerable<Brand> Brands { get; } = new[]
      {
            new Brand { Id = 1, Name = "Acne", Order = 0 , PositionsAmount = 10 },
            new Brand { Id = 2, Name = "Grune Erde", Order = 1 , PositionsAmount = 11},
            new Brand { Id = 3, Name = "Albiro", Order = 2 , PositionsAmount = 1},
            new Brand { Id = 4, Name = "Ronhill", Order = 3 , PositionsAmount = 32},
            new Brand { Id = 5, Name = "Oddmolly", Order = 4 , PositionsAmount = 100},
            new Brand { Id = 6, Name = "Boudestijn", Order = 5 , PositionsAmount = 123},
            new Brand { Id = 7, Name = "Rosch creative culture", Order = 6 , PositionsAmount = 3},
        };

      public static IEnumerable<Product> Products { get; } = new[]
      {
            new Product { Id = 1, Name = "Белое платье", Price = 1025, ImgUrl = "product1.jpg", Order = 0, SectionId = 2, BrandId = 1 },
            new Product { Id = 2, Name = "Розовое платье", Price = 1025, ImgUrl = "product2.jpg", Order = 1, SectionId = 2, BrandId = 1 },
            new Product { Id = 3, Name = "Красное платье", Price = 1025, ImgUrl = "product3.jpg", Order = 2, SectionId = 2, BrandId = 1 },
            new Product { Id = 4, Name = "Джинсы", Price = 1025, ImgUrl = "product4.jpg", Order = 3, SectionId = 2, BrandId = 1 },
            new Product { Id = 5, Name = "Лёгкая майка", Price = 1025, ImgUrl = "product5.jpg", Order = 4, SectionId = 2, BrandId = 2 },
            new Product { Id = 6, Name = "Лёгкое голубое поло", Price = 1025, ImgUrl = "product6.jpg", Order = 5, SectionId = 2, BrandId = 1 },
            new Product { Id = 7, Name = "Платье белое", Price = 1025, ImgUrl = "product7.jpg", Order = 6, SectionId = 2, BrandId = 1 },
            new Product { Id = 8, Name = "Костюм кролика", Price = 1025, ImgUrl = "product8.jpg", Order = 7, SectionId = 25, BrandId = 1 },
            new Product { Id = 9, Name = "Красное китайское платье", Price = 1025, ImgUrl = "product9.jpg", Order = 8, SectionId = 25, BrandId = 1 },
            new Product { Id = 10, Name = "Женские джинсы", Price = 1025, ImgUrl = "product10.jpg", Order = 9, SectionId = 25, BrandId = 3 },
            new Product { Id = 11, Name = "Джинсы женские", Price = 1025, ImgUrl = "product11.jpg", Order = 10, SectionId = 25, BrandId = 3 },
            new Product { Id = 12, Name = "Летний костюм", Price = 1025, ImgUrl = "product12.jpg", Order = 10, SectionId = 24, BrandId = 3 },
            new Product { Id = 13, Name = "Летняя сумка Rosch", Price = 1025, ImgUrl = "product12.jpg", Order = 11, SectionId = 24, BrandId = 7 },
        };
   }
}
