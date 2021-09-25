using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models.Interfaces
{
	public interface IEntity //TODO: Replace it with WebStore.Domain.Entities.Base.Interfaces.IEntity
	{
		int Id { get; set; }
	}
}
