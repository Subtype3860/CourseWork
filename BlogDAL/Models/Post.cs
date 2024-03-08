namespace BlogDAL.Models;

public class Post
{
    public string? Id { get; set; }
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public DateTime DatePublic { get; set; }
    public string? Heading { get; set; }
    public virtual ICollection<PostTag>? PostTags { get; set; }
    public virtual ICollection<Remark>? Remarks { get; set; }
    public int Look { get; set; } = 0;
}