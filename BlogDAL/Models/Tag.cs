namespace BlogDAL.Models;

public class Tag
{
    public string Id { get; set; }
    public string? Stick { get; set; }
    public virtual ICollection<PostTag> PostTags { get; set; }

    public Tag()
    {
        Id = Guid.NewGuid().ToString();
    }
}