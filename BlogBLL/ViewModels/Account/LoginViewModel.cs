using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.Account;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Login", Prompt = "UserName, Email")]
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль", Prompt = "Password")]
    public string? Password { get; set; }

    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}