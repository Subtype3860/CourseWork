namespace BlogBLL.ViewModels.Post;

public class GetPostViewModel
{
    public IEnumerable<BlogDAL.Models.Post>? Posts { get; set; }
}