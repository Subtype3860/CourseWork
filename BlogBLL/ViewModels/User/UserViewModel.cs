using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.User
{
    public class UserViewModel 
    {
        public string? UserId { get; set; }
        
        [Display(Name = "Имя")]
        public string? FirstName { get; set; }
        
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }
        [Display(Name = "Отчество")]
        public string? MiddleName { get; set; }
        
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Эл.адрес")]
        public string? Email { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime? Date { get; set; }
        
        [Display(Name = "Фото профиля")]
        public byte[]? Image { get; set; }
        

    }
}
