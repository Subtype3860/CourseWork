namespace BlogDAL.Models;

public class Tag
{
    public string? Id { get; set; }
    public string? Stick { get; set; }
    public IEnumerable<PostTag> PostTags { get; } = new List<PostTag>();
}