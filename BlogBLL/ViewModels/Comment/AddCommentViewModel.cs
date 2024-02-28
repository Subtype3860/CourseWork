using BlogDAL.Models;

namespace BlogBLL.ViewModels.Comment;

public class AddCommentViewModel
{
    public string? Id { get; set; }
    public string? Comment { get; set; }
    public string? PostId { get; set; }
    public DateTime DatePublic { get; set; }
    public string? UserId { get; set; }
    public byte Log { get; set; }
}