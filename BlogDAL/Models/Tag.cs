namespace BlogDAL.Models;

public class Tag
{
    public string Id { get; set; } = string.Empty;
    public string Stick { get; set; } = string.Empty;
    public IEnumerable<PostTag> PostTags { get; } = new List<PostTag>();
}