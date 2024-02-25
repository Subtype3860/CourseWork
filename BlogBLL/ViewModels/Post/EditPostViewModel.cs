using System.ComponentModel.DataAnnotations;
using BlogDAL.Models;

namespace BlogBLL.ViewModels.Post;

public class EditPostViewModel
{
    public string? PostId { get; set; }
    
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Статья")]
    public string? Post { get; set; }

}