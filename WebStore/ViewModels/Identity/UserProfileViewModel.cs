using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.Identity
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(dataType: DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
