using BlogDAL.Models;

namespace BlogBLL.ViewModels.Post;

public class GetPostViewModel
{
    public string? Id { get; set; }
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public DateTime DatePublic { get; set; }
    public string? Heading { get; set; }
    public virtual IEnumerable<PostTag>? PostTags { get; set; }
    public virtual IEnumerable<Remark>? Remarks { get; set; }
    public bool Log { get; set; }
    public string LoUserId { get; set; }
}