using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebStore.Domain.Entities.Identity
{
	public class User : IdentityUser
	{
		public const string Administrator = "Admin";

		public const string DefaultAdminPassword = "Admin_pass_321";

        public ICollection<Product> FavoriteProducts { get; set; }
	}
}
