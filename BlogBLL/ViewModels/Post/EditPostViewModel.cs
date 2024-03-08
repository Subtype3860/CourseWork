using System.ComponentModel.DataAnnotations;
using BlogDAL.Models;

namespace BlogBLL.ViewModels.Post;

public class EditPostViewModel
{
    [ScaffoldColumn(false)]
    public string? PostId { get; set; }
    [Display(Name = "Заголовок")]
    public string? Heading { get; set; }
    
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Контент")]
    public string? Post { get; set; }
    [ScaffoldColumn(false)]
    public string? UserId { get; set; }
    public BlogDAL.Models.User? User { get; set; }
    [ScaffoldColumn(false)]
    public DateTime DatePublic { get; set; }
    public virtual ICollection<PostTag>? PostTags { get; set; }
    public virtual List<BlogDAL.Models.Tag>? Tags { get; set; }

}