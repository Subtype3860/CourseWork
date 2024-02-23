using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.Tag;

public class EditTagViewModel
{
    public string Id { get; set; }
    [Required(ErrorMessage = "Не указан тэг")]
    [StringLength(30,MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
    public string? Stick { get; set; }
}