namespace BlogDAL.Models;

public class Post
{
    public string Id { get; set; }
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public virtual ICollection<PostTag>? PostTags { get; set; }

    public Post()
    {
        Id = Guid.NewGuid().ToString();
    }
}