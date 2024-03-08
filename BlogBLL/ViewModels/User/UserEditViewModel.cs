using System.ComponentModel.DataAnnotations;
using BlogDAL.Models;

namespace BlogBLL.ViewModels.User
{
    public class UserEditViewModel
    {
        [Required]
        [Display(Name = "Идентификатор пользователя")]
        public string? UserId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string? FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string? LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email", Prompt = "example.com")]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime? BirthDate { get; set; }
        
        [Display(Name = "Имя входа")]
        public string? UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Отчество", Prompt = "Введите отчество")]
        public string? MiddleName { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Новый  пароль")]
        public string? Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string? PasswordConfirm { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Тел. номер")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Статус", Prompt = "Введите статус")]
        public string? Status { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "О себе", Prompt = "Введите данные о себе")]
        public string? About { get; set; }
        public List<AppRole>? AppRoles { get; set; }
        public IList<string>? UserRole { get; set; }
    }
}
