using System.ComponentModel.DataAnnotations;
using BlogDAL.Models;

namespace BlogBLL.ViewModels.Post;

public class EditPostViewModel
{
    [ScaffoldColumn(false)]
    public string? PostId { get; set; }
    public string? Heading { get; set; }
    
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Статья")]
    public string? Post { get; set; }
    public string? Tag { get; set; }
    [ScaffoldColumn(false)]
    public string? UserId { get; set; }
    public User? User { get; set; }
    [ScaffoldColumn(false)]
    public DateTime DatePublic { get; set; }
    public virtual ICollection<PostTag>? PostTags { get; set; }

}