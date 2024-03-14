using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.User
{
    public class CreateUserViewModel
    {
        [EmailAddress]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string? Email { get; set; }
        
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string? Password { get; set; }
    }
}
