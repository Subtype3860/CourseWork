using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.Post;

public class AddPostViewModel
{
    [ScaffoldColumn(false)]
    public string? PostId { get; set; }
    
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Статья")]
    public string? Post { get; set; }
    
    [Required]
    [ScaffoldColumn(false)]
    public string? UserId { get; set; }
    [Required]
    [ScaffoldColumn(false)]
    public DateTime Public { get; set; }
    
    [Display(Name = "Заголовок статьи")]
    public string? Heading { get; set; }

    public IList<BlogDAL.Models.Tag>? Tags { get; set; }
    public IList<string>? TagPostUser { get; set; } 
}