using System.ComponentModel.DataAnnotations;

namespace BlogBLL.ViewModels.Comment;

public class EditCommentViewModel
{
    public string? Id { get; set; }
    [Display(Name = "Комментарий")]
    public string? Comment { get; set; }
    public string? UserId { get; set; }
    public BlogDAL.Models.User? User { get; set; }
    public string? PostId { get; set; }
    public DateTime DatePublic { get; set; }
    public BlogDAL.Models.Post? Post { get; set; }
}