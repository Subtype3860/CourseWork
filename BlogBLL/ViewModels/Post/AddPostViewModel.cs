using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BlogBLL.ViewModels.Post;

public class AddPostViewModel
{
    [ScaffoldColumn(false)]
    [HiddenInput]
    public string? PostId { get; set; }
    
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Статья")]
    public string? Post { get; set; }
    
    [Required]
    [ScaffoldColumn(false)]
    [HiddenInput]
    public string? UserId { get; set; }
    [Required]
    [ScaffoldColumn(false)]
    [HiddenInput]
    public DateTime Public { get; set; }
    
    [Display(Name = "Заголовок статьи")]
    public string? Heading { get; set; }

    public IList<BlogDAL.Models.Tag>? Tags { get; set; }
    public IList<string>? TagPostUser { get; set; } 
}