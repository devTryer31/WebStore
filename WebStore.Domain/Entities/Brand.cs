﻿using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
	public class Brand : NamedEntity, IOrderedEntity
	{
		public int Order { get; set; }

		public int PositionsAmount { get; set; }
	}
}
