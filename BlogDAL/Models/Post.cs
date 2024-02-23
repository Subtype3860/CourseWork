using System.ComponentModel.DataAnnotations;

namespace BlogDAL.Models;

public class Post
{
    public string Id { get; set; }
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public DateTime DatePublic { get; set; }
    public string? Heading { get; set; }
    public virtual List<PostTag>? PostTags { get; } = new ();

    public Post()
    {
        Id = Guid.NewGuid().ToString();
        DatePublic = DateTime.Now;
    }
}