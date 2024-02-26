using BlogDAL.Models;

namespace BlogBLL.ViewModels.Post;

public class GetPostViewModel
{
    public string? Id { get; set; }
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public DateTime DatePublic { get; set; }
    public string? Heading { get; set; }
    public virtual ICollection<PostTag>? PostTags { get; set; }
    public bool Log { get; set; } = false;
}