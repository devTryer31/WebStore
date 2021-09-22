﻿using System.Collections.Generic;
using WebStore.Models.Interfaces;

namespace WebStore.Services.Interfaces
{
	interface IRepository<T> where T : IEntity
	{
		void Add(T item);

		T Get(int id);

		IEnumerable<T> GetAll();

		bool Remove(T item);

		void Update(T item);
	}
}
