using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
	public class User : IdentityUser
	{
		public string Administrator = "Admin";

		public string DefaultAdminPassword = "Admin_pass_321";
	}
}
