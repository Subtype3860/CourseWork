namespace BlogBLL.ViewModels.Post;

public class AddPostWebApi
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public DateTime DatePublic { get; set; } = DateTime.Now;
    public string? Heading { get; set; }
}