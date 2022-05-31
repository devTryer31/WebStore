using System.Collections.Generic;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Services.Interfaces
{
	public interface IRepository<T> where T : IEntity
	{
		void Add(T item);

		T Get(int id);

		IEnumerable<T> GetAll();

		bool Remove(T item);

		void Update(T item);
	}
}
