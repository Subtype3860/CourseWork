using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.Post;

public class AddPostViewModel
{
    public string? PostId { get; set; }
    
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Статья")]
    public string? Post { get; set; }
    
    [Required(ErrorMessage = "Имя пользователя не зарегестрировано")]
    public string? UserId { get; set; }
    
    [Display(Name = "Дата публикации")]
    public DateTime Public { get; set; }
    
    [Display(Name = "Заголовок статьи")]
    public string? Head { get; set; }
}