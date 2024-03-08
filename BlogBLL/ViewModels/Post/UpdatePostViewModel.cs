namespace BlogBLL.ViewModels.Post;

public class UpdatePostViewModel
{
    public string? PostId { get; set; }
    public string? Post { get; set; }
    public List<string>? TagId { get; set; }
}