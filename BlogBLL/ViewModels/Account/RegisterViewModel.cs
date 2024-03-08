using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите Имя")]
        [Display(Name ="Имя пользователя")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Укажите Фамилию")]
        [Display(Name = "Фамилия пользователя")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Неуказан Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Укажите год рождения")]
        [Display(Name = "Год рождения")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [PasswordPropertyText]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [PasswordPropertyText]
        public string? PasswordConfirm { get; set; }
    }
}
