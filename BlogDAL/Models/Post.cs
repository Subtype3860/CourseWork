namespace BlogDAL.Models;

public class Post
{
    public string? Id { get; set; }
    public string Body { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
    public DateTime DatePublic { get; set; } = DateTime.Now;
    public string Heading { get; set; } = string.Empty;
    public virtual ICollection<PostTag>? PostTags { get; set; }
    public virtual ICollection<Remark>? Remarks { get; set; }
    public int Look { get; set; } = 0;
}