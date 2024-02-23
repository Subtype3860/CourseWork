using System.ComponentModel.DataAnnotations;

namespace BlogDAL.Models;

public class Tag
{
    public string? Id { get; set; }
    public string? Stick { get; set; }
    public virtual List<PostTag>? PostTags { get; } = new();

    public Tag()
    {
        Id = Guid.NewGuid().ToString();
    }
}