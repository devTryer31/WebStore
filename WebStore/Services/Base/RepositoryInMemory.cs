using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models.Interfaces;
using WebStore.Services.Interfaces;

namespace WebStore.Services.Base
{
	public abstract class RepositoryInMemory<T> : IRepository<T> where T: IEntity
	{
		//Must not containing null and duplicated values.
		private readonly List<T> _Data;
		
		//Id included in data
		private int _LastId = 1;

		protected RepositoryInMemory()
		{
			_Data = new();
		}

		protected RepositoryInMemory(IEnumerable<T> source)
		{
			_Data = new(source);
			_LastId = _Data.Max(e => e.Id);
		}

		public void Add(T item)
		{
			if (item == null) throw new ArgumentNullException(nameof(item));
			if(_Data.Contains(item)) return; //Only for InMemoryRepository available.

			_Data.Add(item);
			item.Id = ++_LastId;
		}

		public bool Remove(T item)
		{
			if (item == null) throw new ArgumentNullException(nameof(item));
			return _Data.Remove(item);
		}

		public void Update(T item)
		{
			if (item == null) throw new ArgumentNullException(nameof(item));
			var dbItem = Get(item.Id);
			if (dbItem is null) return;
			UpdateItem(item, dbItem);
		}

		public IEnumerable<T> GetAll() => _Data;

		public T Get(int id)
		{
			//if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be a positive number.");
			var source = _Data.SingleOrDefault(x => x.Id == id);
			return source;
		}

		protected abstract void UpdateItem(T source, T destination);
	}
}
