using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.User
{
    public class CreateUserViewModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string? Password { get; set; }
    }
}
