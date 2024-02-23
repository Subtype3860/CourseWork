using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Display(Name = "Запомнить")]
        public bool RemmberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
